using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class SectorRows
    {
        [Key]
        public int SectorRowID { get; set; }
        public int SectorRow { get; set; }
        public int NumberOfSeats { get; set; }
        public int LocationID { get; set; }
        public decimal CostFactor { get; set; }

        public Locations Location { get; set; }

        public ICollection<Tickets> Tickets { get; set; }
    }
}
