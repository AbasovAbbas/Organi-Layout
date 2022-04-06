using IntroSignalR.Models.Entity;
using IntroSignalR.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroSignalR.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;
        readonly UserManager<AppUser> userManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                var foundedUser =await userManager.FindByEmailAsync(model.UserName); 
                if(foundedUser == null)
                {
                    TempData["Message"] = "Bele istifadeci tapilmadi";
                    return View(model);
                }

                var signinResult = await signInManager.PasswordSignInAsync(foundedUser, model.Password, false, true);

                if (!signinResult.Succeeded)
                {
                    TempData["Message"] = "Bele istifadeci tapilmadi";
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
