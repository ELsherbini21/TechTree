using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.RegularExpressions;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.Helpers;
using TechTree.PersantionLayer.ViewModels;

namespace TechTree.PersantionLayer.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var Models = _userManager.Users.Select(user => new ApplicationUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();



            return View(Models);
        }

        public async Task<IActionResult> ManageRoles(string ApplicationUserId)
        {
            var user = await _userManager.FindByIdAsync(ApplicationUserId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRoleViewModel()
            {
                ApplicationUserId = user.Id,

                ApplicationUserName = user.UserName,

                Roles = roles.Select(role => new RoleSelectViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result

                }).ToList()

            };

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.ApplicationUserId);

            if (user == null)
                return NotFound();


            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoleViewModel.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string userId)
        {
            if (userId is null)
                return BadRequest();

            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
                return NotFound();

            var appUserViewModel = new ApplicationUserViewModel()
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                UserName = appUser.UserName,
                Email = appUser.Email
            };

            return View(appUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserViewModel appUserViewModel)
        {

            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(appUserViewModel.Id);

                if (appUser is not null)
                {
                    var DummyListOfUser = new List<ApplicationUser>() { appUser };

                    var checkEmailDuplicate = _userManager.Users.ToList().Except(DummyListOfUser, new AppUserEmail_EqualityComparer()).Any(appUser => appUser.Email== appUserViewModel.Email);

                    var checkUserNameDuplicate = _userManager.Users.ToList().Except(DummyListOfUser, new AppUserUserName_EqualityComparer()).Any(appUser => appUser.UserName == appUserViewModel.UserName);

                    if (checkEmailDuplicate == false && checkUserNameDuplicate == false)
                    {
                        appUser.FirstName = appUserViewModel.FirstName;
                        appUser.LastName = appUserViewModel.LastName;
                        appUser.UserName = appUserViewModel.UserName;
                        appUser.Email = appUserViewModel.Email;

                        await _userManager.UpdateAsync(appUser);

                        return RedirectToAction(nameof(Index) , "Home");
                    }


                }
            }

            return View(appUserViewModel);
        }
    }
}
