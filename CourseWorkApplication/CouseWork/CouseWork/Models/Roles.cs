using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public required string RoleName { get; set; }
        public ICollection<Users> Users { get; set; }
        public ICollection<Organizers> Organizers { get; set; }
        public ICollection<Managers> Managers { get; set; }
    }
}
