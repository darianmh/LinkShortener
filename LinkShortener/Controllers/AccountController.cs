using System;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Classes.Mapper;
using LinkShortener.Data.User;
using LinkShortener.Services.User;
using LinkShortener.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    public class AccountController : MainBaseController
    {
        #region Fields

        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSigninManager _applicationSigninManager;
        private readonly IUserTokenService _userTokenService;

        #endregion
        #region Methods

        public IActionResult Index(string ReturnUrl)
        {
            var model = new LoginRegisterViewModel() { RedirectUrl = ReturnUrl, IsLogin = true };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.HasMessage = true;
                return View("Index", model);
            }
            var user = await _applicationUserManager.FindAsync(model.LoginName);
            if (user == null)
            {
                model.HasMessage = true;
                ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
                return View("Index", model);
            }
            var result = await _applicationSigninManager.PasswordSignInAsync(user, model.LoginPassword, false, false);
            if (result.Succeeded)
                return Redirect(model.RedirectUrl ?? "/");
            model.HasMessage = true;
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.HasMessage = true;
                return View("Index", model);
            }
            var user = new ApplicationUser()
            {
                Email = model.Email,
                Family = model.Family,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@').First()
            };
            var result = await _applicationUserManager.CreateAsync(user, model.Password);
            if (result.Succeeded) return RedirectToAction("Index");
            foreach (var identityError in result.Errors)
            {
                model.HasMessage = true;
                ModelState.AddModelError(identityError.Code, identityError.Description);
            }
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.HasMessage = true;
                return View("Index", model);
            }
            var user = await _applicationUserManager.FindAsync(model.ForgetName);
            if (user == null)
            {
                model.HasMessage = true;
                ModelState.AddModelError("", "کاربر یافت نشد.");
                return View("Index", model);
            }

            var token = await _applicationUserManager.GenerateToken(user, UserTokenType.ResetPassword);
            var body = $"کد شما برای بازیابی رمز عبور: {token}";
            var subject = $"کاربر عزیر {_applicationUserManager.GetUSerDisplayNameAsync(user)}";
            return View(new ForgotPasswordModel
            {
                UserId = user.Id
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmForgotPassword(ForgotPasswordModel model)
        {
            var user = await _applicationUserManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد.");
                return View("ForgotPassword", model);
            }

            var check = await _userTokenService.CheckUserToken(user.Id, UserTokenType.ResetPassword, model.Token);
            if (!check)
            {
                ModelState.AddModelError("", "کد وارد شده صحیح نمی باشد.");
                return View("ForgotPassword", model);
            }

            var result = await _applicationUserManager.ResetPasswordAsync(user, model.Password);
            if (result)
            {
                await _userTokenService.RemoveUserToken(user.Id, UserTokenType.ResetPassword);
                return MessagePage("رمز عبور شما با موفقیت تغییر کرد");
            }
            ModelState.AddModelError("", "نام کاربر مورد تایید نمی باشد.");
            return View("ForgotPassword", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(ApplicationUserModel model)
        {
            if (User.Identity == null) return NotFound();
            var user = await _applicationUserManager.FindAsync(User.Identity.Name);
            if (user == null) return NotFound();
            user.Name = model.Name;
            user.Family = model.Family;
            if (model.PhoneNumber != null && !model.PhoneNumber.Equals(user.PhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                user.PhoneNumber = model.PhoneNumber;
                user.PhoneNumberConfirmed = false;
            }
            if (model.Email != null && !model.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
            {
                user.Email = model.Email;
                user.EmailConfirmed = false;
            }
            await _applicationUserManager.UpdateAsync(user);
            //password
            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.CurrentPassword))
            {
                var result = await _applicationUserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    HttpContext.Items.Add("Errors", "نام کاربری مورد تایید نیست.");
                }
            }
            return RedirectToAction("Profile");
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            if (User.Identity == null) return NotFound();
            var user = await _applicationUserManager.FindAsync(User.Identity.Name);
            if (user == null) return NotFound();
            var model = user.ToModel();
            if (HttpContext.Items.ContainsKey("Errors"))
            {
                var error = HttpContext.Items["Errors"];
                ModelState.AddModelError("", error?.ToString() ?? "");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _applicationSigninManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        #endregion
        #region Utilities


        #endregion
        #region Ctor

        public AccountController(ApplicationUserManager applicationUserManager, ApplicationSigninManager applicationSigninManager, IUserTokenService userTokenService)
        {
            _applicationUserManager = applicationUserManager;
            _applicationSigninManager = applicationSigninManager;
            _userTokenService = userTokenService;
        }

        #endregion
    }
}
