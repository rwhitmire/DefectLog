using DefectLog.Core.Services;
using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Validation.Framework;

namespace DefectLog.Web.Validation
{
    public class AdminUserFormValidator : IValidator<AdminUserForm>
    {
        private readonly IUserService _userService;

        public AdminUserFormValidator(IUserService userService)
        {
            _userService = userService;
        }

        public ValidationResult Validate(AdminUserForm form)
        {
            var result = new ValidationResult();

            if (!_userService.UserNameAvailable(form.Id, form.UserName))
                result.AddError<AdminUserForm>(x => x.UserName, "User name is in use");

            return result;
        }
    }
}