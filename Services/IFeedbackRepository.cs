using PieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Services
{
    public interface IFeedbackRepository
    {
        void AddFeedback(Feedback feedback);
    }
}
