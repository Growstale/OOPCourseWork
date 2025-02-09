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
    public class ManageAccountViewModel : INotifyPropertyChanged
    {
        #region Fields

        UnitOfWork unitOfWork;

        public ICommand LocationCommand { get; }
        public ICommand CategoryCommand { get; }
        public ICommand RowsCommand { get; }
        public ICommand BlockCommand { get; }
        public ICommand TicketRefundCommand { get; }
        public ICommand QuestionCommand { get; }
        public ICommand EventCommand { get; }
        public ICommand EventScheduleCommand { get; }
        public ICommand EditQuestionCommand {  get; }
        public ICommand UsersCommand { get; }
        public ICommand OrganizersCommand { get; }

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

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        #region Methods
        public ManageAccountViewModel()
        {
            unitOfWork = new UnitOfWork();
            LocationCommand = new RelayCommand(_ => LocationView());
            CategoryCommand = new RelayCommand(_ => CategoryView());
            RowsCommand = new RelayCommand(_ => RowsView());
            BlockCommand = new RelayCommand(_ => BlockView());
            TicketRefundCommand = new RelayCommand(_ => RefundView());
            QuestionCommand = new RelayCommand(_ => QuestionView());
            EventCommand = new RelayCommand(_ => EventView());
            EventScheduleCommand = new RelayCommand(_ => EventScheduleView());
            UsersCommand = new RelayCommand(_ => UsersView());
            OrganizersCommand = new RelayCommand(_ => OrganizersView());
            EditQuestionCommand = new RelayCommand(_ => QuestionView());

        }

        private void LocationView()
        {
            CurrentView = new LocationEdit();
        }
        private void CategoryView()
        {
            CurrentView = new CategoryEdit();
        }
        private void RowsView()
        {
            CurrentView = new SectorRowEdit();
        }
        private void BlockView()
        {
            CurrentView = new LocationEdit();
        }
        private void RefundView()
        {
            CurrentView = new TicketRefundEdit();
        }
        private void QuestionView()
        {
            CurrentView = new QuestionEdit();
        }
        private void EventView()
        {
            CurrentView = new EventEditManager();
        }
        private void EventScheduleView()
        {
            CurrentView = new EventScheduleEditManager();
        }
        private void UsersView()
        {
            CurrentView = new UserEdit();
        }
        private void OrganizersView()
        {
            CurrentView = new OrganizerEdit();
        }
        #endregion
    }
}
