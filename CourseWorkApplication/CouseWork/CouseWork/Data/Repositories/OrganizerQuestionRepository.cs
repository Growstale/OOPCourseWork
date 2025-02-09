using CouseWork.Context;
using CouseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CouseWork.Data.Repositories
{
    public class OrganizerQuestionRepository : BaseRepository<OrganizerQuestionRepository>
    {
        ApplicationDbContext _context;
        public OrganizerQuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ObservableCollection<OrganizerQuestions> GetOrganizerQuestions(int organizeID)
        {
            var questions = _context.OrganizerQuestions
                .Where(uq => uq.OrganizerID == organizeID && !string.IsNullOrWhiteSpace(uq.AnswerText))
                .ToList();

            return new ObservableCollection<OrganizerQuestions>(questions);
        }
        public ObservableCollection<OrganizerQuestions> GetOrganizerQuestionsForManager(int managerID)
        {
            var questions = _context.OrganizerQuestions
                .Where(uq => uq.ManagerID == managerID)
                .ToList();

            return new ObservableCollection<OrganizerQuestions>(questions);
        }
        public void AddQuestion(int organizerID, string questionText)
        {
            var maxQuestionId = CheckNextQuestionId();

            var newQuestion = new OrganizerQuestions
            {
                QuestionID = maxQuestionId,
                OrganizerID = organizerID,
                QuestionText = questionText,
                Status = "In processing",
                ManagerID = GetManagerWithLeastQuestions(),
                QuestionDate = DateTime.Now
            };

            _context.OrganizerQuestions.Add(newQuestion);
            _context.SaveChanges();
        }
        public void AddAnswer(int answerID, string newAnswer)
        {
            var question = _context.OrganizerQuestions.FirstOrDefault(q => q.QuestionID == answerID);
            if (question.Status != "Close")
                question.AnswerText = (question.AnswerText ?? string.Empty) + newAnswer;
            _context.SaveChanges();
        }
        public void Close(int answerID)
        {
            var question = _context.OrganizerQuestions.FirstOrDefault(q => q.QuestionID == answerID);
            if (question.Status != "Close")
                question.Status = "Close";
            _context.SaveChanges();
        }
        public bool CheckLength(int answerID, string newAnswer)
        {
            var question = _context.OrganizerQuestions.FirstOrDefault(q => q.QuestionID == answerID);
            int oldLength = question?.AnswerText?.Length ?? 0;
            if (oldLength + newAnswer.Length > 500)
            {
                MessageBox.Show((string)Application.Current.Resources["item85"]);
                return false;
            }
            return true;
        }


    }
}
