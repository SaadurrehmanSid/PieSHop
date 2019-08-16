using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PieShop.Data_Access_Layer;
using PieShop.Models;

namespace PieShop.Services
{
    public class PieRepository : IPieRepository
    {
        private AppDbContext _context;

        public PieRepository(AppDbContext context)
        {
            _context = context; 
        }
        public IEnumerable<Pie> GetAllPies()
        {
            return _context.Pies;
        }

        public Pie GetPie(int id)
        {
            return _context.Pies.Include(p=>p.PieReviews).FirstOrDefault(p => p.Id == id);
        }
    }
}
