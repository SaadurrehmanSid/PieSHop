using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PieShop.Data_Access_Layer;
using PieShop.ViewModels;
using PieShop.Models;

namespace PieShop.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> _usermanager { get; }

        private AppDbContext _dbcontext;

        public AccountController(AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _usermanager = userManager;
            _dbcontext = dbContext;
        }

        public async Task<IActionResult> DeleteUser(string id) {
            var user =await _usermanager.FindByIdAsync(id);
                      await _usermanager.DeleteAsync(user);

            return RedirectToAction("index","account");
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
                    UserName = regUser.Name,
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

        

       
    }
}