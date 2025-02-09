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
    public class CommentsRepository : BaseRepository<Comments> 
    {
        ApplicationDbContext _context;
        public CommentsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Comments> GetCommentsByEventId(int eventId)
        {
            return _context.Comments.Where(c => c.EventID == eventId).Include(c => c.User).ToList();
        }

        public void Add(Comments comment)
        {
            _context.Comments.Add(comment);
        }
        public int FindMaxId()
        {
            return _context.Comments.Max(e => (int?)e.CommentID) + 1 ?? 1;
        }

    }
}
