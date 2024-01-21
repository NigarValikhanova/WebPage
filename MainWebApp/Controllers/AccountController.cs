using Azure.Identity;
using MainWebApp.DAL;
using MainWebApp.Extensions;
using MainWebApp.Models;
using MainWebApp.StaticData;
using MainWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace MainWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appContext;

        public AccountController(AppDbContext appContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _appContext = appContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Register() //login ve register acsin deye
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new AppUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
                await _signInManager.SignInAsync(user, isPersistent:false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View(model);

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                
                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials");
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if(email=="" || email==null || !email.Contains("@"))
            {
                ModelState.AddModelError(string.Empty, "Email format is not correct");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if(user!= null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Account", new { email = email, token }, Request.Scheme);
                await _emailService.SendEmailAsync(email, "Reset Password", resetLink);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public async Task <IActionResult> ResetPassword(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            ResetPasswordVM reset = new ResetPasswordVM
            {
                Token = token,
                Email = email
            };
            return View(reset);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetVM)
        {
            if(!ModelState.IsValid)
            {
                return View(resetVM);
            }
            var user = await _userManager.FindByEmailAsync(resetVM.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, resetVM.Token, resetVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }
    }
}
