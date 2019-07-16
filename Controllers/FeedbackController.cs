using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.Services;

namespace PieShop.Controllers
{
    public class FeedbackController : Controller
    {
        private IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _feedbackRepository.AddFeedback(feedback);
                return RedirectToAction("FeedbackSuccess");
            }
            return View("Index",feedback);
        }


        public IActionResult FeedbackSuccess()
        {
            return View();
        }
    }
}