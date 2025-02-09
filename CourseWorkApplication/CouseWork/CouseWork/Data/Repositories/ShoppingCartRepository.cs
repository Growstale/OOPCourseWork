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
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<ShoppingCart> Get(System.Func<ShoppingCart, bool> predicate, string includeProperties = "")
        {
            IQueryable<ShoppingCart> query = _context.ShoppingCart;

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).ToList();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _context.ShoppingCart.ToList();
        }

        public ShoppingCart GetByID(int id)
        {
            return _context.ShoppingCart.Find(id);
        }

        public void Insert(ShoppingCart cartItem)
        {
            _context.ShoppingCart.Add(cartItem);
        }

        public void Update(ShoppingCart cartItem)
        {
            _context.Entry(cartItem).State = EntityState.Modified;
        }

        public void Delete(int shoppingCartId)
        {
            var item = _context.ShoppingCart.FirstOrDefault(cart => cart.ShoppingCartID == shoppingCartId);
            if (item != null)
            {
                _context.ShoppingCart.Remove(item);
            }
        }

        public int FindMaxId()
        {
            return _context.ShoppingCart.Max(e => (int?)e.ShoppingCartID) + 1 ?? 1;
        }
        public IEnumerable<ShoppingCart> GetAllByUserId(int userId)
        {
            return _context.ShoppingCart
                .Where(cart => cart.UserID == userId)
                .Include(cart => cart.Ticket) 
                .ToList();
        }
        public void ClearCartByUserId(int userId)
        {
            var items = _context.ShoppingCart.Where(cart => cart.UserID == userId).ToList();
            _context.ShoppingCart.RemoveRange(items);
        }
    }
}
