using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DefectLog.Core.Models;
using DefectLog.Core.Services;
using DefectLog.Web.Forms;
using DefectLog.Web.Validation.Framework;
using DefectLog.Web.ViewModels;

namespace DefectLog.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterForm> _registerFormValidator;
        private readonly IValidator<ChangePasswordForm> _changePasswordFormValidator; 

        public AccountController(IUserService userService, IValidator<RegisterForm> registerFormValidator, 
            IValidator<ChangePasswordForm> changePasswordFormValidator)
        {
            _userService = userService;
            _registerFormValidator = registerFormValidator;
            _changePasswordFormValidator = changePasswordFormValidator;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            var defectsFixed = _userService.GetFixedCountForUser(User.Identity.Name);
            var defectsLogged = _userService.GetLoggedCountForUser(User.Identity.Name);

            var viewModel = new AccountDashboardViewModel
            {
                DefectsFixed = defectsFixed,
                DefectsLogged = defectsLogged
            };

            return View(viewModel);
        }

        public ActionResult UserProfile()
        {
            var user = _userService.GetCurrent(User.Identity.Name);
            var form = Mapper.Map<UserProfileForm>(user);

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserProfileForm form)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetCurrent(User.Identity.Name);
                Mapper.Map(form, user);
                _userService.Update(user);
            }

            return View(form);
        }

        public ActionResult ChangePassword()
        {
            var form = new ChangePasswordForm();
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordForm form)
        {
            // prevent hackers!
            form.UserName = User.Identity.Name;

            _changePasswordFormValidator.Validate(form).Populate(ModelState);

            if (ModelState.IsValid)
            {
                _userService.ChangePassword(form.UserName, form.NewPassword);
                return View("ChangePasswordSuccess");
            }

            return View(form);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var form = new LoginForm {ReturnUrl = returnUrl};
            return View(form);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginForm form)
        {
            var authorized = _userService.Authenticate(form.UserName, form.Password);

            if (authorized)
            {
                FormsAuthentication.SetAuthCookie(form.UserName, true);
                return RedirectToLocal(form.ReturnUrl);
            }

            ModelState.AddModelError("", "User name and password invalid");
            return View(form);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var form = new RegisterForm();
            return View(form);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterForm form)
        {
            _registerFormValidator.Validate(form).Populate(ModelState);

            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(form);
                _userService.Register(user);
                return RedirectToAction("RegisterComplete");
            }

            return View(form);
        }

        [AllowAnonymous]
        public ActionResult RegisterComplete()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordForm form)
        {
            if (Request.Url == null) throw new Exception("Request URL could not be identified");

            if (ModelState.IsValid)
            {
                var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                _userService.SendResetPasswordLink(form.UserName, baseUrl);
                return RedirectToAction("ForgotPasswordEmailSent");
            }

            return View(form);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordEmailSent()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(Guid id)
        {
            var user = _userService.GetUserByResetPasswordKey(id);
            if (user == null) return new HttpNotFoundResult();

            var form = new ResetPasswordForm(id);
            return View(form);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordForm form)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByResetPasswordKey(form.ResetPasswordKey);
                if (user == null) return new HttpNotFoundResult();

                _userService.ResetPassword(user, form.Password);
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                return RedirectToAction("Index", "Home");
            }

            return View(form);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
