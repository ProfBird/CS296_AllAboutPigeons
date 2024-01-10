using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AllAboutPigeons.Models;

namespace AllAboutPigeons.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> userMngr)
        {
            userManager = userMngr;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username, Name = model.Name };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                  return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors) 
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

       

      

      

        public ViewResult AccessDenied()
        {
            return View();
        }

       
      

    }
}