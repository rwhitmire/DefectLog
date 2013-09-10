using DefectLog.Core.Services;
using DefectLog.Web.Forms;
using DefectLog.Web.Validation.Framework;

namespace DefectLog.Web.Validation
{
    public class ChangePasswordFormValidator : IValidator<ChangePasswordForm>
    {
        private readonly IUserService _userService;

        public ChangePasswordFormValidator(IUserService userService)
        {
            _userService = userService;
        }

        public ValidationResult Validate(ChangePasswordForm form)
        {
            var result = new ValidationResult();

            if (!_userService.Authenticate(form.UserName, form.CurrentPassword))
                result.AddError<ChangePasswordForm>(x => x.CurrentPassword, "Incorrect password");

            return result;
        }
    }
}