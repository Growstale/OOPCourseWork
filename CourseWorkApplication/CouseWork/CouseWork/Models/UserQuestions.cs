using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public class UserQuestions : IQuestion
    {
        [Key]
        public int QuestionID { get; set; }
        public int UserID { get; set; }
        public required string QuestionText { get; set; }
        public DateTime QuestionDate { get; set; }
        public string? AnswerText { get; set; }
        public required string Status { get; set; }
        public int ManagerID { get; set; }
        public string QuestionType => "User";


        public Users User { get; set; }
        public Managers Manager { get; set; }
    }
}
