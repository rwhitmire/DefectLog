using System;
using System.Data.Entity;
using System.Linq;
using DefectLog.Domain;
using DefectLog.Models;

namespace DefectLog.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAll();
        IQueryable<User> GetAllForDisplay();
        IQueryable<User> GetAllDeleted();
        IQueryable<User> NilApproved();
        User Get(int id);
        User GetCurrent(string username);
        User GetUserByResetPasswordKey(Guid resetPasswordKey);
        void Update(User user);
        void Delete(User user);
        void Restore(User user);
        bool IsUserInRole(string username, string rolename);
        string[] GetRolesForUser(string username);
        void Register(User user);
        bool UserNameAvailable(int id, string userName);
        bool Authenticate(string username, string password);
        void SendResetPasswordLink(string userName, string baseUrl);
        void ResetPassword(User user, string password);
        int GetFixedCountForUser(string username);
        int GetLoggedCountForUser(string username);
        void ChangePassword(string username, string newPassword);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IEmailService _emailService;
        private readonly ICryptographyService _cryptographyService;

        public UserService(IRepository<User> repository, IRepository<Role> roleRepository, IEmailService emailService, ICryptographyService cryptographyService)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _emailService = emailService;
            _cryptographyService = cryptographyService;
        }

        public IQueryable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<User> GetAllForDisplay()
        {
            return GetAll().Where(x => !x.IsDeleted)
                .Where(x => x.UserName != "Admin")
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName);
        }

        public IQueryable<User> GetAllDeleted()
        {
            return GetAll().Where(x => x.IsDeleted);
        }

        public IQueryable<User> NilApproved()
        {
            return GetAllForDisplay().Where(x => x.IsApproved == null);
        }

        public User Get(int id)
        {
            return _repository.Get(id);
        }

        public User GetCurrent(string username)
        {
            return _repository.GetAll().FirstOrDefault(x => x.UserName == username);
        }

        public User GetUserByResetPasswordKey(Guid resetPasswordKey)
        {
            return _repository.GetAll().FirstOrDefault(x => x.ResetPasswordKey == resetPasswordKey);
        }

        public void Update(User user)
        {
            _repository.Update(user);
            _repository.Save();
        }

        public void Delete(User user)
        {
            user.IsDeleted = true;
            _repository.Update(user);
            _repository.Save();
        }

        public void Restore(User user)
        {
            user.IsDeleted = false;
            _repository.Update(user);
            _repository.Save();
        }

        public bool IsUserInRole(string username, string rolename)
        {
            var user = _repository.GetAll().FirstOrDefault(x => x.UserName == username);
            if (user == null) return false;

            return user.Role.RoleName == rolename;
        }

        public string[] GetRolesForUser(string username)
        {
            var user = _repository.GetAll().FirstOrDefault(x => x.UserName == username);
            if (user == null) return new string[] {};

            return new[] {user.Role.RoleName};
        }

        public void Register(User user)
        {
            var defaultRole = _roleRepository.GetAll().Single(x => x.RoleName == "User");

            user.PasswordSalt = _cryptographyService.GetSalt();
            user.Password = _cryptographyService.HashPassword(user.Password, user.PasswordSalt);
            user.RoleId = defaultRole.Id;

            _repository.Insert(user);
            _repository.Save();
        }

        public bool UserNameAvailable(int id, string userName)
        {
            var user = _repository.GetAll()
                .Where(x => x.Id != id)
                .SingleOrDefault(x => x.UserName == userName);

            return user == null;
        }

        public bool Authenticate(string username, string password)
        {
            var user = _repository.GetAll().FirstOrDefault(x => x.UserName == username);
            
            if (user == null) return false;
            if (user.IsApproved != true) return false;
            if (user.IsDeleted) return false;

            return _cryptographyService.HashPassword(password, user.PasswordSalt) == user.Password;
        }

        public void SendResetPasswordLink(string userName, string baseUrl)
        {
            var user = _repository.GetAll().SingleOrDefault(x => x.UserName == userName);

            if (user == null)
                throw new Exception("The provided userName does not exist.");

            user.ResetPasswordKey = Guid.NewGuid();

            _repository.Update(user);
            _repository.Save();
            _emailService.SendPasswordReset(user.EmailAddress, user.ResetPasswordKey, baseUrl);
        }

        public void ResetPassword(User user, string password)
        {
            user.PasswordSalt = _cryptographyService.GetSalt();
            user.Password = _cryptographyService.HashPassword(password, user.PasswordSalt);
            user.ResetPasswordKey = null;

            _repository.Update(user);
            _repository.Save();
        }

        public int GetFixedCountForUser(string username)
        {
            var user = _repository.GetAll()
                .Include(x => x.DeveloperDefects)
                .SingleOrDefault(x => x.UserName == username);

            if (user == null) return 0;

            return user.DeveloperDefects.Count(x => x.Status.Name == "Fixed");
        }

        public int GetLoggedCountForUser(string username)
        {
            var user = _repository.GetAll()
                .Include(x => x.TesterDefects)
                .SingleOrDefault(x => x.UserName == username);

            if (user == null) return 0;

            return user.TesterDefects.Count();
        }

        public void ChangePassword(string username, string newPassword)
        {
            var user = GetCurrent(username);
            if (user == null) return;

            var salt = _cryptographyService.GetSalt();
            var hashedPassword = _cryptographyService.HashPassword(newPassword, salt);

            user.PasswordSalt = salt;
            user.Password = hashedPassword;

            _repository.Update(user);
            _repository.Save();
        }
    }
}