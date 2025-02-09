using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Organizers
    {
        [Key]
        public int OrganizerID { get; set; }
        public required string CompanyName { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public int RoleID { get; set; }
        public Roles Role { get; set; }

        public ICollection<Events> Events { get; set; }
        public ICollection<OrganizerQuestions> OrganizerQuestions { get; set; }
    }
}
