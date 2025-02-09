using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartID { get; set; }
        public int UserID { get; set; }
        public int TicketID { get; set; }

        public Users User { get; set; }
        public Tickets Ticket { get; set; }
    }
}
