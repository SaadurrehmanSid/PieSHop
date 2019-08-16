using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Data_Access_Layer;
using PieShop.Models;
using PieShop.Services;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private AppDbContext _db;
        private IPieRepository _pieRepository;
        private IPieReviewRepository _pieReviewRepository;
        private HtmlEncoder _htmlEncoder;

        public PieController(AppDbContext db,IPieRepository pieRepository,
                         IPieReviewRepository pieReviewRepository,HtmlEncoder htmlEncoder)
        {
            _db = db;
            _pieRepository = pieRepository;
            _pieReviewRepository = pieReviewRepository;
            _htmlEncoder = htmlEncoder;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPie(id);

           
            return View(new DetailPieVM { Pie = pie });
        }


        [HttpPost]
        public IActionResult Details(int id, string review)
        {
            var pie = _pieRepository.GetPie(id);
            if (pie == null)
                return NotFound();

            string encodedReview = _htmlEncoder.Encode(review);

            _pieReviewRepository.AddReview(new PieReview { Pie = pie, Review = encodedReview });

            return View (new DetailPieVM() { Pie = pie, Review = String.Empty });
        }
    }
}