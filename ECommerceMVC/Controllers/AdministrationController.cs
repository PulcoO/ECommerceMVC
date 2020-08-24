using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ECommerceMVC.Models.Clients;
using ECommerceMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerceMVC.Controllers
{
    [Authorize(Roles = "Super Administrator")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Client> _userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<Client> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListRole()
        {
            //ce n'est pas une méthode
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name);
                if (!roleExist)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = model.Name
                    };
                    IdentityResult result = await _roleManager.CreateAsync(identityRole);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "home");
                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                
            }

            return View(model);
        }

        

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage =$"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            foreach (Client user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editmodel)
        {
            var role = await _roleManager.FindByIdAsync(editmodel.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {editmodel.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = editmodel.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(editmodel);
            }

        }
        [HttpGet]
        public async Task<IActionResult> EditRoleOfUsers(string roleId)
        {
            ViewBag.roleId = roleId; // plutot que de l'avoir dans la classe : UserRoleViewModel

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            List<UserRoleViewModel> model = new List<UserRoleViewModel>();

            foreach(Client user in _userManager.Users)
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRoleOfUsers(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for(int i=0; i <model.Count; i++)
            {
                Client user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult identityResult = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)) )
                {
                    identityResult = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    //dans le cas ou il est selectionner alors qu'il est déja dans le role
                    continue;
                }

                if (identityResult.Succeeded)
                {
                    if(i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
