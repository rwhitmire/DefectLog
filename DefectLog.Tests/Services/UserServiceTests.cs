using System;
using System.Collections.Generic;
using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;
using DefectLog.Services;
using Moq;
using Xunit;

namespace DefectLog.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _repository;
        private readonly UserService _service;
        private readonly Mock<IRepository<Role>> _roleRepository;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ICryptographyService> _cryptographyService;

        public UserServiceTests()
        {
            _repository = new Mock<IRepository<User>>();
            _roleRepository = new Mock<IRepository<Role>>();
            _emailService = new Mock<IEmailService>();
            _cryptographyService = new Mock<ICryptographyService>();

            _service = new UserService(_repository.Object, _roleRepository.Object, _emailService.Object, _cryptographyService.Object);
        }

        [Fact]
        public void CreateUserShouldEncryptPassword()
        {
            var roles = new List<Role>
            {
                new Role {Id = 1, RoleName = "User"},
                new Role {Id = 2, RoleName = "Admin"}
            };

            var user = new User
            {
                Password = "password"
            };

            _repository.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(u => user = u);
            _roleRepository.Setup(x => x.GetAll()).Returns(roles.AsQueryable());
            _cryptographyService.Setup(x => x.GetSalt()).Returns("passwordsalt");
            _cryptographyService.Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<string>())).Returns("hashedpassword");
            
            _service.Register(user);

            Assert.Equal("hashedpassword", user.Password);
            Assert.Equal("passwordsalt", user.PasswordSalt);
        }

        [Fact]
        public void AuthenticateShouldAcceptValidPassword()
        {
            const string username = "user";
            const string password = "password";
            const string salt = "salt";

            var users = new List<User>
            {
                new User
                {
                    UserName = username, 
                    Password = password, 
                    PasswordSalt = salt,
                    IsApproved = true
                }
            };

            _cryptographyService.Setup(x => x.GetSalt()).Returns(salt);
            _cryptographyService.Setup(x => x.HashPassword(password, salt)).Returns(password);
            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result =_service.Authenticate(username, password);
            Assert.True(result);
        }

        [Fact]
        public void AuthenticateShouldRejectInvalidPassword()
        {
            const string username = "user";
            const string password = "password";
            const string salt = "salt";

            _cryptographyService.Setup(x => x.GetSalt()).Returns(salt);
            _cryptographyService.Setup(x => x.HashPassword(password, salt)).Returns(password);

            var users = new List<User>
            {
                new User
                {
                    UserName = username, 
                    Password = password, 
                    PasswordSalt = salt,
                    IsApproved = true
                }
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.Authenticate(username, "wrong password");
            Assert.False(result);
        }

        [Fact]
        public void AuthenticateShouldRejectNonExistantUser()
        {
            const string username = "user";
            const string password = "password";
            const string salt = "salt";

            _cryptographyService.Setup(x => x.GetSalt()).Returns(salt);
            _cryptographyService.Setup(x => x.HashPassword(password, salt)).Returns(password);

            var users = new List<User>
            {
                new User
                {
                    UserName = username, 
                    Password = password,
                    PasswordSalt = salt,
                    IsApproved = true
                }
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.Authenticate("non existant user", "wrong password");
            Assert.False(result);
        }

        [Fact]
        public void AuthenticateShouldRejectDeletedUser()
        {
            const string username = "user";
            const string password = "password";
            const string salt = "salt";

            _cryptographyService.Setup(x => x.GetSalt()).Returns(salt);
            _cryptographyService.Setup(x => x.HashPassword(password, salt)).Returns(password);

            var users = new List<User>
            {
                new User
                {
                    UserName = username, 
                    Password = password, 
                    PasswordSalt = salt,
                    IsDeleted = true,
                    IsApproved = true
                }
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.Authenticate("user", "password");
            Assert.False(result);
        }

        [Fact]
        public void AuthenticateShouldRejectUnapprovedUser()
        {
            const string username = "user";
            const string password = "password";
            const string salt = "salt";

            _cryptographyService.Setup(x => x.GetSalt()).Returns(salt);
            _cryptographyService.Setup(x => x.HashPassword(password, salt)).Returns(password);

            var users = new List<User>
            {
                new User
                {
                    UserName = username, 
                    Password = password, 
                    PasswordSalt = salt,
                    IsApproved = false
                }
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.Authenticate("user", "password");
            Assert.False(result);
        }

        [Fact]
        public void IsUserInRoleShouldReturnTrueWhenUserIsInRole()
        {
            var users = new List<User>
            {
                new User {UserName = "bill", Role = new Role {RoleName = "user"}}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.IsUserInRole("bill", "user");
            Assert.True(result);
        }

        [Fact]
        public void IsUserInRoleShouldReturnFalseWhenUserIsNotInRole()
        {
            var users = new List<User>
            {
                new User {UserName = "bill", Role = new Role {RoleName = "user"}}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.IsUserInRole("bill", "admin");
            Assert.False(result);
        }

        [Fact]
        public void IsUserInRoleShouldReturnFalseWhenUserDoesNotExist()
        {
            var users = new List<User>
            {
                new User {UserName = "bill", Role = new Role {RoleName = "user"}}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.IsUserInRole("ron", "admin");
            Assert.False(result);
        }

        [Fact]
        public void GetRolesForUserShouldReturnCorrectRoles()
        {
            var users = new List<User>
            {
                new User {UserName = "bill", Role = new Role {RoleName = "user"}}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.GetRolesForUser("bill");
            Assert.Equal(new[] {"user"}, result);
        }

        [Fact]
        public void GetRolesForNullUserShouldReturnEmptyArray()
        {
            var users = new List<User>
            {
                new User {UserName = "bill", Role = new Role {RoleName = "user"}}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.GetRolesForUser("joe");
            Assert.Equal(new string[] {}, result);
        }

        [Fact]
        public void UserNameAvailableReturnsFalseWhenUserNameUnavailableForNewUser()
        {
            var users = new List<User>
            {
                new User {Id = 1, UserName = "bill"}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.UserNameAvailable(0, "bill");
            Assert.False(result);
        }

        [Fact]
        public void UserNameAvailableReturnsTrueWhenUserNameAvailableForNewUser()
        {
            var users = new List<User>
            {
                new User {Id = 1, UserName = "bill"}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.UserNameAvailable(0, "jim");
            Assert.True(result);
        }

        [Fact]
        public void RegisterShouldSetDefaultRole()
        {
            var roles = new List<Role>
            {
                new Role {Id = 1, RoleName = "User"},
                new Role {Id = 2, RoleName = "Admin"}
            };

            var user = new User
            {
                UserName = "bill",
                Password = "password"
            };
            
            _roleRepository.Setup(x => x.GetAll()).Returns(roles.AsQueryable());
            _repository.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(u => user = u);
            _service.Register(user);

            Assert.Equal(1, user.RoleId);
        }

        [Fact]
        public void SendResetPasswordLinkShouldAddGuidToUser()
        {
            var users = new List<User>
            {
                new User {UserName = "bill"}
            };

            var user = new User();

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());
            _repository.Setup(x => x.Update(It.IsAny<User>())).Callback<User>(u => user = u);
            _service.SendResetPasswordLink("bill", "url");

            Assert.NotNull(user.ResetPasswordKey);
        }

        [Fact]
        public void SendResetPasswordLinkThrowsExceptionWhenUserInvalid()
        {
            var users = new List<User>
            {
                new User {UserName = "bill"}
            };

            var user = new User();

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());
            _repository.Setup(x => x.Update(It.IsAny<User>())).Callback<User>(u => user = u);

            var ex = Assert.Throws<Exception>(() => _service.SendResetPasswordLink("bogus user", "url"));

            Assert.Equal("The provided userName does not exist.", ex.Message);
            Assert.Null(user.ResetPasswordKey);
        }

        [Fact]
        public void ResetPassword()
        {
            var user = new User
            {
                Password = "password",
                PasswordSalt = "salt",
                ResetPasswordKey = Guid.NewGuid()
            };

            _repository.Setup(x => x.Update(It.IsAny<User>())).Callback<User>(u => user = u);
            _service.ResetPassword(user, "newpassword");

            Assert.NotEqual("password", user.Password);
            Assert.NotEqual("salt", user.PasswordSalt);
            Assert.Null(user.ResetPasswordKey);

            // new password should be encrypted
            Assert.NotEqual("newpassword", user.Password);
        }

        [Fact]
        public void GetUnapprovedUsersShouldReturnUsersWhereIsApprovedIsNull()
        {
            var users = new List<User>
            {
                new User {Id = 1, IsApproved = null},
                new User {Id = 2, IsApproved = false},
                new User {Id = 3, IsApproved = true},
                new User {Id = 4, IsApproved = null}
            };

            _repository.Setup(x => x.GetAll()).Returns(users.AsQueryable());

            var result = _service.NilApproved();
            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.ToList()[0].Id);
            Assert.Equal(4, result.ToList()[1].Id);
        }
    }
}
