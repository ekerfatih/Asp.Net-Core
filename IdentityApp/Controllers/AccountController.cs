using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers {
    public class AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager) : Controller {

        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<AppRole> _roleManager = roleManager;
        private SignInManager<AppUser> _signInManager = signInManager;
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null) {
                    await _signInManager.SignOutAsync();

                    if (!await _userManager.IsEmailConfirmedAsync(user)) {
                        ModelState.AddModelError("", "Hesabınızı onaylayınız");
                        return View(model);
                    }


                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                    if (result.Succeeded) {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);

                        return RedirectToAction("Index", "Home");
                    } else if (result.IsLockedOut) {
                        var lockeOutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockeOutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("", $"Hesabınız Kitlendi,Lütfen {timeLeft.Minutes} dakika sonra deneyiniz. ");
                    } else {
                        ModelState.AddModelError("", "Paronlanız hatalı");
                    }
                } else {
                    ModelState.AddModelError("", "bu email adıyla bir hesap bulunmadı");
                }

            }

            return View(model);
        }

        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model) {

            if (ModelState.IsValid) {
                var user = new AppUser {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Account", new { user.Id, token });

                    //email

                    TempData["message"] = "Email hesabınızdaki onay mailini tıklayın";
                    return RedirectToAction("Login","Account");
                }
                foreach (IdentityError err in result.Errors) {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token) {
            if (Id == null || token == null) {
                TempData["message"] = "Geçersiz token bilgisi";
                return View();
            }
            var user = await _userManager.FindByIdAsync(Id);

            if (user != null) {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded) {
                    TempData["message"] = "Hesabınız onaylandı";
                    return RedirectToAction("Login","Account");
                }
            }
            TempData["message"] = "Kullanıcı bulunamadı";
            return View();
        }


    }
}