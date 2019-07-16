using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PieShop.Data_Access_Layer;
using PieShop.Models;

namespace PieShop.Services
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private AppDbContext _context;

        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddFeedback(Feedback feedback)
        {

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
           

        }
    }
}
