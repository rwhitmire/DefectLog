using DefectLog.Areas.Admin.Forms;
using DefectLog.Forms;
using DefectLog.Validation;
using DefectLog.Validation.Framework;
using Ninject.Modules;

namespace DefectLog.Modules
{
    public class ValidationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<RegisterForm>>().To<RegisterFormValidator>();
            Bind<IValidator<AdminAppVersionForm>>().To<AdminAppVersionFormValidator>();
            Bind<IValidator<ChangePasswordForm>>().To<ChangePasswordFormValidator>();
        }
    }
}