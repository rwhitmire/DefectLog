using DefectLog.Core.Services;
using DefectLog.Web.Forms;
using DefectLog.Web.Validation.Framework;

namespace DefectLog.Web.Validation
{
    public class RegisterFormValidator : IValidator<RegisterForm>
    {
        private readonly IUserService _userService;

        public RegisterFormValidator(IUserService userService)
        {
            _userService = userService;
        }

        public ValidationResult Validate(RegisterForm form)
        {
            var result = new ValidationResult();

            if (!_userService.UserNameAvailable(form.Id, form.UserName))
                result.AddError<RegisterForm>(x => x.UserName, "User name is not available");

            return result;
        }
    }
}