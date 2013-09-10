using DefectLog.Core.Services;
using DefectLog.Web.Forms;
using DefectLog.Web.Validation;
using Moq;
using Xunit;

namespace DefectLog.Tests.Validation
{
    public class RegisterFormValidatorTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly RegisterFormValidator _validator;

        public RegisterFormValidatorTests()
        {
            _userService = new Mock<IUserService>();
            _validator = new RegisterFormValidator(_userService.Object);
        }

        [Fact]
        public void ReturnErrorWhenUserNameNotUnique()
        {
            _userService.Setup(x => x.UserNameAvailable(It.IsAny<int>(), It.IsAny<string>())).Returns(false);

            var form = new RegisterForm();
            var result = _validator.Validate(form);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void DoNotReturnErrorWhenUserNameUnique()
        {
            _userService.Setup(x => x.UserNameAvailable(It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            
            var form = new RegisterForm();
            var result = _validator.Validate(form);

            Assert.True(result.IsValid);
        }
    }
}
