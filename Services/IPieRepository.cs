using PieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Services
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAllPies();
        Pie GetPie(int id);
    }
}
