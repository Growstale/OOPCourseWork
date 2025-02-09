using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public int RoleID { get; set; }

        public Roles Role { get; set; }

        public ICollection<Sales> Sales { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<UserQuestions> UserQuestions { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public ICollection<TicketRefund> TicketRefunds { get; set; }
    }
}
