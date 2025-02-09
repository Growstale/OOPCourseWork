using CouseWork.Context;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Data.Repositories
{
    public class SalesRepository : BaseRepository<Sales>
    {
        ApplicationDbContext _context;
        public SalesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Add(Sales sale)
        {
            _context.Sales.Add(sale);
        }
        public int FindMaxId()
        {
            return _context.Sales.Max(e => (int?)e.SaleID) + 1 ?? 1;
        }
        public IEnumerable<Sales> GetAllByUserId(int userId)
        {
            return _context.Sales
                .Where(s => s.UserID == userId)
                .ToList();
        }

        public void Update(Sales entity)
        {
            _context.Sales.Update(entity);
        }
        public Sales GetById(int saleId)
        {
            return _context.Sales.Include(s => s.Ticket).Include(s => s.User).FirstOrDefault(s => s.SaleID == saleId); 
        }
        public List<Sales> GetAll()
        {
            return _context.Sales.ToList();
        }
    }
}
