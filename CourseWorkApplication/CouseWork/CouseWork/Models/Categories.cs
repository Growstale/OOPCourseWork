using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
        public ICollection<Events> Events { get; set; }
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
