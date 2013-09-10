using DefectLog.Core.Services;
using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Validation.Framework;

namespace DefectLog.Web.Validation
{
    public class AdminAppVersionFormValidator : IValidator<AdminAppVersionForm>
    {
        private readonly IAppVersionService _appVersionService;

        public AdminAppVersionFormValidator(IAppVersionService appVersionService)
        {
            _appVersionService = appVersionService;
        }

        public ValidationResult Validate(AdminAppVersionForm form)
        {
            var result = new ValidationResult();

            if (_appVersionService.VersionExists(form.Id, form.VersionNumber))
                result.AddError<AdminAppVersionForm>(x => x.VersionNumber, "Version already exists");

            return result;
        }
    }
}