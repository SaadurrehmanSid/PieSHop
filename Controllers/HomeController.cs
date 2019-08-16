using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Services;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class HomeController : Controller
    {
        private IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }
        public IActionResult Index()
        {
            var pie = _pieRepository.GetAllPies().OrderBy(p=>p.Name);
            var HomeModel = new HomeViewModel
            {
                Pies = pie.ToList(),
                Title = "Welcome to Bethany's Pie Shop"
            };
            return View(HomeModel);
        }
      


    }
}