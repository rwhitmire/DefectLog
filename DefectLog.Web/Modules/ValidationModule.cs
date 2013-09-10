using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Forms;
using DefectLog.Web.Validation;
using DefectLog.Web.Validation.Framework;
using Ninject.Modules;

namespace DefectLog.Web.Modules
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