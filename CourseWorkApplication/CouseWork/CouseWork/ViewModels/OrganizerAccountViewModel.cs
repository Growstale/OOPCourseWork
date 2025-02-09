using CouseWork.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CouseWork.Commands;
using CouseWork.Views;

namespace CouseWork.ViewModels
{
    public class OrganizerAccountViewModel : INotifyPropertyChanged
    {
        UnitOfWork unitOfWork;
        public ICommand EventCommand { get; }
        public ICommand EventScheduleCommand { get; }
        public ICommand AskQuestionCommand { get; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        public OrganizerAccountViewModel()
        {
            unitOfWork = new UnitOfWork();
            EventCommand = new RelayCommand(_ => EventView());
            EventScheduleCommand = new RelayCommand(_ => EventScheduleView());
            AskQuestionCommand = new RelayCommand(_ => AskQuestionView());
        }

        private void EventView()
        {
            CurrentView = new EventEdit();
        }
        private void EventScheduleView()
        {
            CurrentView = new EventScheduleEdit();
        }
        private void AskQuestionView()
        {
            CurrentView = new AskQuestion();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
