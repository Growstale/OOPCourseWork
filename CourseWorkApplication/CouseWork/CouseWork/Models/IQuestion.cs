using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Models
{
    public interface IQuestion
    {
        int QuestionID { get; }
        string QuestionText { get; }
        DateTime QuestionDate { get; }
        string? AnswerText { get; }
        string Status { get; }
        int ManagerID { get; }
        string QuestionType { get; }
    }
}
