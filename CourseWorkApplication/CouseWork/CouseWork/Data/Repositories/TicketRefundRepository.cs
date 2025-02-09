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
    public class TicketRefundRepository : BaseRepository<TicketRefund>
    {
        ApplicationDbContext _context;
        public TicketRefundRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Add(TicketRefund entity)
        {
            _context.TicketRefund.Add(entity);
        }
        public TicketRefund GetByID(int id)
        {
            return _context.TicketRefund.Find(id);
        }

        public void Delete(TicketRefund entity)
        {
            _context.TicketRefund.Remove(entity);
        }

        public IEnumerable<TicketRefund> GetAllBySaleId(int saleId)
        {
            return _context.TicketRefund
                .Where(tr => tr.SaleID == saleId)
                .ToList();
        }
        public int FindMaxId()
        {
            return _context.TicketRefund.Max(e => (int?)e.TicketRefundID) + 1 ?? 1;
        }
        public IEnumerable<TicketRefund> GetAll()
        {
            return _context.TicketRefund.ToList();
        }

    }
}
