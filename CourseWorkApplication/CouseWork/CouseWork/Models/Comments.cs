using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public string? Login { get; set; }
        public int EventID { get; set; }
        public required string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int FivePointRating { get; set; }

        public Users User { get; set; }

        public Events Event { get; set; }
    }
}
