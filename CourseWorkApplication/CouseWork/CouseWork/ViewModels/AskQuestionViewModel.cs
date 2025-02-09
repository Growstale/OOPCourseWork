using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using CouseWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CouseWork.ViewModels
{
    public class AskQuestionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly UnitOfWork _unitOfWork;

        private string _question;
        private string _questionError;
        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
                QuestionError = string.Empty;
            }
        }
        public string QuestionError
        {
            get { return _questionError; }
            set
            {
                _questionError = value;
                OnPropertyChanged(nameof(QuestionError));
            }
        }
        public ObservableCollection<OrganizerQuestions> OrganizerAnswers { get; set; }

        public ICommand AskCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        public AskQuestionViewModel()
        {
            _unitOfWork = new UnitOfWork();
            AskCommand = new RelayCommand(AskQuestion, CanAskQuestion);
            OrganizerAnswers = new ObservableCollection<OrganizerQuestions>(_unitOfWork.OrganizerQuestionRepository.GetOrganizerQuestions(UserSession.CurrentUserID));

        }
        private bool CanAskQuestion(object parameter)
        {
            var canExecute = !string.IsNullOrEmpty(Question);
            return canExecute;
        }
        private void AskQuestion(object parameter)
        {
            if (!string.IsNullOrEmpty(Question) && Question.Length < 500)
            {
                _unitOfWork.OrganizerQuestionRepository.AddQuestion(UserSession.CurrentUserID, Question);
                _unitOfWork.Save();
                MessageBox.Show((string)Application.Current.Resources["item37"]);
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["item86"]);
            }
        }
        #endregion
    }
}
