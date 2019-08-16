using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class PieReview
    {
        public int PieReviewId { get; set; }
        public Pie Pie { get; set; }
        public string Review { get; set; }
    }
}
