using CommercialShop.Models;
using CommercialShop.Models.ChangePasswordDTOs;
using CommercialShop.Models.LoginDTOs;
using CommercialShop.Models.RegisterDTOs;
using CommercialShop.Models.VerifyEmailDTOs;
using CommercialShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


namespace CommercialShop.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly IEmailService _emailService;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task< IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid) return View(model);

            // check email confirmation before attempting sign-in
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(nameof(model.Email), "Please confirm your email before logging in.");
                TempData["ConfirmAtLogin"] = "true";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded) return RedirectToAction("Index", "Home");

            ModelState.AddModelError(nameof(model.Password), "Email or Password is not recognised");
            return View(model);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email
                };

                var emailCheck = await _userManager.FindByEmailAsync(model.Email);

                if (emailCheck != null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Email is duplicate");
                    return View(model); // show error
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("VerifyEmail", "Account", new { email = model.Email });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);

                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult VerifyEmail(VerifyEmail model, string? email = null)

        {
            model.Email = email ?? string.Empty;  // prefill if passed from Register
          
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmail model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(nameof(model.Email), "User not found");
                return View(model);
            }

            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
                var subject = "Confrim Link";
                var body = $"Please confirm your email here: <a href='{confirmationLink}'>Confirm Link<a/>";

                await _emailService.SendEmailAsync(model.Email, subject, body);
            }
            return RedirectToAction("VerifyEmailSent", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                // 🎉 EmailConfirmed = true is now written to database
                TempData["EmailConfirmedMessage"] = "Your email has been confirmed! You can now log in.";

                
                return RedirectToAction("Login", "Account");
            }

            return View("EmailConfirmationFailed");
        }


        public async Task<IActionResult> VerifyEmailSent()
        {
            return View();
        }

       

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> EmailForChangePassword(VerifyEmail model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null){
               ModelState.AddModelError(nameof(model.Email), "Email not found");
               return View(model);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //If you don’t encode it, the confirmation link may break because the token contains characters like /, +, =.
            var tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var resetLink = Url.Action("ChangePassword", "Account", new { email = model.Email, token = tokenEncoded }, protocol: HttpContext.Request.Scheme);
            var subject = "Reset Password Link";
            var body = $"Please click here to reset password: <a href='{resetLink}'>Reset Password<a/>";
            await _emailService.SendEmailAsync(model.Email, subject, body);

            return RedirectToAction("VerifyEmailSent","Account");
        }


        public IActionResult EmailForChangePassword()
        {
            return View();
        }


        // GET: show ChangePassword form, accept token and email from the link
        [HttpGet]
        public IActionResult ChangePassword(string? email = null, string? token = null)
        {
            var model = new ChangePassword
            {
                Email = email ?? string.Empty,
                Token = token ?? string.Empty
            };
            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var decodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["PasswordChanged"] = "Password has been updated";
                return RedirectToAction("Login", "Account");
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);

        }

    }
}
