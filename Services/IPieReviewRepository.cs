

using PieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Services
{
    public interface IPieReviewRepository
    {
        void AddReview(PieReview pieReview );
        IEnumerable<PieReview> GetPieReviews(int pieId);
    }
}
