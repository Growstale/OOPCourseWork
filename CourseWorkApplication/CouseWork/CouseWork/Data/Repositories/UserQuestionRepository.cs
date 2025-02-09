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
    public class UserQuestionRepository : BaseRepository<UserQuestions>
    {
        ApplicationDbContext _context;
        public UserQuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ObservableCollection<UserQuestions> GetUserQuestions(int userID)
        {
            var questions = _context.UserQuestions
                .Where(uq => uq.UserID == userID && !string.IsNullOrWhiteSpace(uq.AnswerText))
                .ToList();

            return new ObservableCollection<UserQuestions>(questions);
        }
        public ObservableCollection<UserQuestions> GetUserQuestionsForManager(int managerID)
        {
            var questions = _context.UserQuestions
                .Where(uq => uq.ManagerID == managerID)
                .ToList();

            return new ObservableCollection<UserQuestions>(questions);
        }
        public void AddQuestion(int userID, string questionText)
        {
            var maxQuestionId = CheckNextQuestionId();

            var newQuestion = new UserQuestions
            {
                QuestionID = maxQuestionId,
                UserID = userID,
                QuestionText = questionText,
                Status = "In processing",
                ManagerID = GetManagerWithLeastQuestions(),
                QuestionDate = DateTime.Now
            };

            _context.UserQuestions.Add(newQuestion);
            _context.SaveChanges();
        }

        public void AddAnswer(int answerID, string newAnswer)
        {
            var question = _context.UserQuestions.FirstOrDefault(q => q.QuestionID == answerID);
            if (question.Status != "Close")
                question.AnswerText = (question.AnswerText ?? string.Empty) + newAnswer;
            _context.SaveChanges();
        }
        public void Close(int answerID)
        {
            var question = _context.UserQuestions.FirstOrDefault(q => q.QuestionID == answerID);
            if (question.Status != "Close")
                question.Status = "Close";
            _context.SaveChanges();
        }
        public bool CheckLength(int answerID, string newAnswer)
        {
            var question = _context.UserQuestions.FirstOrDefault(q => q.QuestionID == answerID);
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
