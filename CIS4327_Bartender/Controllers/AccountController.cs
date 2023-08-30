using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CIS4327_Bartender.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = new AccountIndexModel
            {
                UserList = _userManager.Users
            };

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginAccountModel
            {
                ReturnUrl = returnUrl
            };


            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAccount(LoginAccountModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user,
                        model.Password, false, false)).Succeeded)
                    {
                        TempData["message"] = "Logged in successfully";
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return RedirectToAction("Index", "Home");
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        //POST: Account/Create 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(LoginAccountModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                Console.WriteLine(model.Password);
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false); 
                    TempData["message"] = "Account created successfully";
                    await _userManager.AddToRoleAsync(user, "Customer");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.TryAddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["message"] = "User deleted successfully";
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    AddErrors(result);
                }
            }
            else
            {
                TempData["message"] = "User Not Found";
                // ModelState.AddModelError("", "User Not Found");
            }
            
            return View("Index", _userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            AppUser oldUser = await _userManager.FindByIdAsync(id);
            if (oldUser != null)
            {
                var model = new EditAccountModel
                {
                    TempUserId = id,
                    TempSecurityStamp = oldUser.SecurityStamp,
                    OldUserEmail = oldUser.Email,
                    OldUserName = oldUser.UserName

                };

                return View(model);
            }

            TempData["message"] = "User Not Found";
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> EditAccount (EditAccountModel model)
        {
            // Console.WriteLine("Test1 " + model.NewUserName + model.NewUserEmail);
            AppUser oldUser = await _userManager.FindByIdAsync(model.TempUserId);
            if (oldUser != null) 
            {
                oldUser.UserName = model.NewUserName;
                oldUser.Email = model.NewUserEmail;

                await _userManager.UpdateAsync(oldUser);

                TempData["message"] = "User has been successfully edited";
                return RedirectToAction("Index", "Account");
            }

            TempData["message"] = "User Not Found";
            return RedirectToAction("Index", "Account");

        }

        public async Task<IActionResult> Role(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roleNameList = await _userManager.GetRolesAsync(user);

                var roleName = roleNameList.FirstOrDefault();

                var model = new ChangeRoleAccountModel
                {
                    UserId = user.Id.ToString(),
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    CurrentRoleName = roleName.ToString()

                };

                return View(model);
            }

            TempData["message"] = "User Not Found";
            return RedirectToAction("Index", "Admin");
        }


        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(ChangeRoleAccountModel model)
        {
            Console.WriteLine("Test: " + model.UserId + " AND " + model.SelectedRoleName);
            AppUser user = await _userManager.FindByIdAsync(model.UserId); // gets the user whos role is being changed

            if (user != null)
            {
                var oldRoleNameList = await _userManager.GetRolesAsync(user); // gets a list of all the roles the user is assigned to
                var oldRoleName = oldRoleNameList.FirstOrDefault(); // gets the first element of the list, there should only be one role per user

                if ((oldRoleName != null) && !oldRoleName.Equals(model.SelectedRoleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, oldRoleName);
                    TempData["Message"] = "Role for user: " + model.UserName + " has been updated from " + oldRoleName + " to " + model.SelectedRoleName;
                    await _userManager.AddToRoleAsync(user, model.SelectedRoleName);
                    return RedirectToAction("Index", "Account");
                }
                
            }
            TempData["Message"] = "No change to user role";
            return RedirectToAction("Index", "Account");
        }
    }
}
