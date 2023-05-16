using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Art_Gallery.Models;
using Art_Gallery.BLL.Services.Contracts;
using Art_Gallery.DAL.Models;
using System.Text;
using System.Diagnostics;
using Art_Gallery.DAL;

namespace Art_Gallery.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserService _userService;

        public AccessController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Privacy", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {   
            int userRole = _userService.UserSignIn(modelLogin.Email, modelLogin.Password);//make UserSignIn return 0 for invalid user, 1 for admin and 2 for regular user
            if (userRole > 0)//valid user
            {
                List<Claim> claims = new List<Claim>(){
                             new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                             new Claim("OtherProperties", "Example Role")
                         };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    //IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                TempData["Email"] = modelLogin.Email;//This is how you pass data to subsequent controllers and views

                if(userRole == 1) //ADMIN
                {
                    return RedirectToAction("ShowCategories", "Admin");//Admin CONTROLLER
                }
                else if (userRole == 2) //USER
                {
                    return RedirectToAction("ShowFilters", "User");//User CONTROLLER
                }
            }

            ViewData["ValidateMessage"] = ConstantStrings.userNotFound;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register() // sau public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(VMRegister modelRegister)
        {
            try
            {
                User u = new User
                {
                    Name = modelRegister.Name,
                    Email = modelRegister.Email,
                    Password = modelRegister.Password,
                };

                await _userService.Register(u);
                await Login(new VMLogin { Email = u.Email, Password = u.Password, KeepLoggedIn = false });

                TempData["Email"] = modelRegister.Email;// SAVE EMAIL IN TEMPDATA

                return RedirectToAction("ShowFilters", "User");
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.completeAllFields });
            }
        }
    }
}
