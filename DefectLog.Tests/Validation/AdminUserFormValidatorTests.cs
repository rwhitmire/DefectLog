using DefectLog.Areas.Admin.Forms;
using DefectLog.Services;
using DefectLog.Validation;
using Moq;
using Xunit;

namespace DefectLog.Tests.Validation
{
    public class AdminUserFormValidatorTests
    {
        private readonly AdminUserFormValidator _validator;
        private readonly Mock<IUserService> _userService;

        public AdminUserFormValidatorTests()
        {
            _userService = new Mock<IUserService>();
            _validator = new AdminUserFormValidator(_userService.Object);
        }

        [Fact]
        public void UserNameShouldHaveErrorWhenInUse()
        {
            _userService.Setup(x => x.UserNameAvailable(It.IsAny<int>(), It.IsAny<string>())).Returns(false);

            var form = new AdminUserForm();
            var result = _validator.Validate(form);
            result.ShouldHaveError<AdminUserForm>(x => x.UserName, "User name is in use");
        }
    }
}
