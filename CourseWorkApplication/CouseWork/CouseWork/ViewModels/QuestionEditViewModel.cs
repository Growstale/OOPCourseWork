using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CouseWork.ViewModels
{
    public class QuestionEditViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly UnitOfWork _unitOfWork;

        public ObservableCollection<IQuestion> _questions;
        public ObservableCollection<IQuestion> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        private IQuestion _selectedQuestion;
        public IQuestion SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));

                if (value != null)
                {
                    QuestionID = value.QuestionID.ToString();
                    QuestionText = value.QuestionText;
                    QuestionDate = value.QuestionDate.ToString();
                    Status = value.Status;
                    QuestionType = value.QuestionType;
                    AnswerText = string.Empty;
                }
                else
                {
                    ResetFields();
                }
            }

        }
        private string _questionID;
        public string QuestionID
        {
            get => _questionID;
            set
            {
                _questionID = value;
                OnPropertyChanged(nameof(QuestionID));
            }
        }

        private string _questionText;
        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                OnPropertyChanged(nameof(QuestionText));
            }
        }

        private string _questionDate;
        public string QuestionDate
        {
            get => _questionDate;
            set
            {
                _questionDate = value;
                OnPropertyChanged(nameof(QuestionDate));
            }
        }

        private string? _answerText;
        public string? AnswerText
        {
            get => _answerText;
            set
            {
                _answerText = value;
                OnPropertyChanged(nameof(AnswerText));
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _questionType;
        public string QuestionType
        {
            get => _questionType;
            set
            {
                _questionType = value;
                OnPropertyChanged(nameof(_questionType));
            }
        }

        public ICommand AnswerCommand { get; }
        public ICommand CloseCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods
        public QuestionEditViewModel()
        {
            _unitOfWork = new UnitOfWork();

            var organizerQuestions = _unitOfWork.OrganizerQuestionRepository
                .GetOrganizerQuestionsForManager(UserSession.CurrentUserID);

            var userQuestions = _unitOfWork.UserQuestionRepository
                .GetUserQuestionsForManager(UserSession.CurrentUserID);

            var combinedQuestions = organizerQuestions
                .Cast<IQuestion>()
                .Concat(userQuestions.Cast<IQuestion>())
                .ToList();

            Questions = new ObservableCollection<IQuestion>(combinedQuestions);
            AnswerCommand = new RelayCommand(AddAnswer, CanChangeQuestion);
            CloseCommand = new RelayCommand(CloseQuestion, CanChangeQuestion);
        }
        private void ResetFields()
        {
            QuestionID = string.Empty;
            QuestionText = string.Empty;
            QuestionDate = string.Empty;
            AnswerText = null;
            Status = string.Empty;
        }
        private void RefreshAnswers()
        {
            var combinedQuestions = _unitOfWork.OrganizerQuestionRepository.GetOrganizerQuestionsForManager(UserSession.CurrentUserID)
                .Cast<IQuestion>()
                .Concat(_unitOfWork.UserQuestionRepository.GetUserQuestionsForManager(UserSession.CurrentUserID).Cast<IQuestion>())
                .ToList();

            Questions = new ObservableCollection<IQuestion>(combinedQuestions);
        }
        private void AddAnswer(object parameter)
        {
            if (Status == "In processing")
            {
                if (QuestionType == "Organizer" && _unitOfWork.OrganizerQuestionRepository.CheckLength(int.Parse(QuestionID), AnswerText))
                {
                    _unitOfWork.OrganizerQuestionRepository.AddAnswer(int.Parse(QuestionID), AnswerText);
                }
                else if (QuestionType == "User" && _unitOfWork.UserQuestionRepository.CheckLength(int.Parse(QuestionID), AnswerText))
                {
                    _unitOfWork.UserQuestionRepository.AddAnswer(int.Parse(QuestionID), AnswerText);
                }
                RefreshAnswers();
            }
        }
        private void CloseQuestion(object parameter)
        {
            if (Status == "In processing")
            {
                if (QuestionType == "Organizer" && _unitOfWork.OrganizerQuestionRepository.CheckLength(int.Parse(QuestionID), AnswerText))
                {
                    _unitOfWork.OrganizerQuestionRepository.AddAnswer(int.Parse(QuestionID), AnswerText);
                    _unitOfWork.OrganizerQuestionRepository.Close(int.Parse(QuestionID));
                }
                else if (QuestionType == "User" && _unitOfWork.UserQuestionRepository.CheckLength(int.Parse(QuestionID), AnswerText))
                {
                    _unitOfWork.UserQuestionRepository.AddAnswer(int.Parse(QuestionID), AnswerText);
                    _unitOfWork.UserQuestionRepository.Close(int.Parse(QuestionID));
                }
                RefreshAnswers();
            }
        }

        private bool CanChangeQuestion(object parameter)
        {
            return !string.IsNullOrEmpty(AnswerText) && Status == "In processing";
        }
        #endregion
    }
}
