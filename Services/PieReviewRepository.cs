using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PieShop.Data_Access_Layer;
using PieShop.Models;

namespace PieShop.Services
{
    public class PieReviewRepository : IPieReviewRepository
    {
        private AppDbContext _db;

        public PieReviewRepository(AppDbContext db)
        {
            _db = db;
        }
        public void AddReview(PieReview pieReview)
        {
            _db.PieReviews.Add(pieReview);
            _db.SaveChanges();
        }

        public IEnumerable<PieReview> GetPieReviews(int pieId)
        {
          return  _db.PieReviews.Where(p =>p.Pie.Id == pieId);
        }
    }
}
