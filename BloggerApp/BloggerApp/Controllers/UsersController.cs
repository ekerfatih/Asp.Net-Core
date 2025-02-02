using BloggerApp.Data.Abstract;
using BloggerApp.Data.Concrete.EfCore;
using BloggerApp.Entity;
using BloggerApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace BloggerApp.Controllers {
    public class UsersController(IUserRepository userRepository) : Controller {
        private readonly IUserRepository _userRepository = userRepository;

        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                var isUser = _userRepository.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (isUser != null) {
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));

                    if (isUser.Email == "info@sadikturan.com") {
                        userClaims.Add(new Claim(ClaimTypes.Role, isUser.Name ?? "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { 
                        IsPersistent = true,
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index","Posts");

                } else {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            }
            return View(model);
        }
    }
}