using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PieShop.Data_Access_Layer;
using PieShop.ViewModels;
using PieShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace PieShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> _usermanager { get; }

        private AppDbContext _dbcontext;
        public RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext dbContext, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _usermanager = userManager;
            _dbcontext = dbContext;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> DeleteUser(string id) {
            var user = await _usermanager.FindByIdAsync(id);
            await _usermanager.DeleteAsync(user);

            return RedirectToAction("index", "account");
        }


        public IActionResult Index()
        {
            var users = _dbcontext.Users;
            return View(users);
        }
       


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user =await _usermanager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        [HttpPost]
        public async Task<IActionResult> EditUser(string id,ApplicationUser updatedUser)
        {
            var user = await _usermanager.FindByIdAsync(id);
            if (ModelState.IsValid) {
                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;

             var result =  await _usermanager.UpdateAsync(user);
                if(result.Succeeded)
                  return RedirectToAction("index","account");
            }

            return View(updatedUser);
        }



        [HttpGet]
        public IActionResult CreateUser() {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser(adminRegUser regUser)
        {
            if (!ModelState.IsValid)
            {
                return View(regUser);
            }
            else
            {
                var user = new ApplicationUser
                {
                    FullName= regUser.Name, 
                    UserName = regUser.Email,
                    Email = regUser.Email,
                    PhoneNumber = regUser.Phone,
                    Address = regUser.Address,
                    City= regUser.City,
                    Country = regUser.Country
                   
                };

                var result = await _usermanager.CreateAsync(user, regUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
            }
            return View(regUser);
        }



        [HttpGet]
        public IActionResult CreateRole() {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (ModelState.IsValid) {
                IdentityRole identityRole = new IdentityRole {
                    Name = model.Name
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles", "Account");
                }
                else {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }

            return View(model);
        }


        public IActionResult GetRoles() {
            var roles = _roleManager.Roles;
            return View(roles);
        }



        [HttpGet]
        public async Task<IActionResult> EditRole(string id) {

            var role =await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new EditRoleVM {
                Id = id,
                Name = role.Name
            };

            foreach (var user in _usermanager.Users)
            {
                if (await _usermanager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.FullName);
                }
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVM model) {

            var role =await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                role.Name = model.Name;
              var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("getroles","account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }



        public async Task<IActionResult> DeleteRole(string id)
        {
            var role =await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

           var result = await _roleManager.DeleteAsync(role);
            return RedirectToAction("getroles","account");
        }




        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var model = new List<EditUsersInRoleVM>();

            foreach (var user  in _usermanager.Users)
            {
                var userRoleVM = new EditUsersInRoleVM {

                    UserId =  user.Id,
                    UserName = user.UserName
                };
                if (await _usermanager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else {
                    userRoleVM.IsSelected = false;
                }

                model.Add(userRoleVM);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<EditUsersInRoleVM> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            foreach (var userOne in model)
            {
                var user =await  _usermanager.FindByIdAsync(userOne.UserId);
                IdentityResult result = null;

                if (userOne.IsSelected && !(await _usermanager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _usermanager.AddToRoleAsync(user, role.Name);
                }
                else if (!userOne.IsSelected && await _usermanager.IsInRoleAsync(user, role.Name))
                {
                    result = await _usermanager.RemoveFromRoleAsync(user, role.Name);
                }


            }
            return RedirectToAction("EditRole","Account", new {Id = roleId }); ;
        }
    }

}