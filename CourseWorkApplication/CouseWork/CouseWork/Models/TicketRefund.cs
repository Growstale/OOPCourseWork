using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class TicketRefund
    {
        [Key]
        public int TicketRefundID { get; set; }

        public int SaleID { get; set; }
        public int UserID { get; set; }
        public DateTime RefundDate { get; set; }

        public Sales Sale { get; set; }
        public Users User { get; set; }
    }
}
