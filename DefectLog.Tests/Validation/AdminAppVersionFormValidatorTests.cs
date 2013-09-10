using DefectLog.Core.Services;
using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Validation;
using Moq;
using Xunit;

namespace DefectLog.Tests.Validation
{
    public class AdminAppVersionFormValidatorTests
    {
        private readonly AdminAppVersionFormValidator _validator;
        private readonly Mock<IAppVersionService> _appVersionService;

        public AdminAppVersionFormValidatorTests()
        {
            _appVersionService = new Mock<IAppVersionService>();
            _validator = new AdminAppVersionFormValidator(_appVersionService.Object);
        }

        [Fact]
        public void VersionNumberShouldHaveErrorWhenNotUnique()
        {
            _appVersionService.Setup(x => x.VersionExists(It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            var result = _validator.Validate(new AdminAppVersionForm());
            result.ShouldHaveError<AdminAppVersionForm>(x => x.VersionNumber, "Version already exists");
        }
    }
}
