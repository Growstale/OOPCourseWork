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
    public class TicketsRepository : BaseRepository<Tickets>
    {
        ApplicationDbContext _context;
        public TicketsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Tickets> Get(System.Func<Tickets, bool> predicate, string includeProperties = "")
        {
            IQueryable<Tickets> query = _context.Tickets;

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).ToList();
        }
        public IEnumerable<Tickets> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public Tickets GetByID(int id)
        {
            return _context.Tickets.Find(id);
        }

        public void Insert(Tickets ticket)
        {
            _context.Tickets.Add(ticket);
        }

        public void Update(Tickets ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Tickets ticket = _context.Tickets.Find(id);
            if (ticket != null)
                _context.Tickets.Remove(ticket);
        }
    }
}
