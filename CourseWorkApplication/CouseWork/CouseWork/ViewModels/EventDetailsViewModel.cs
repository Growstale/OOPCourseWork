using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using CouseWork.Utilities;
using CouseWork.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace CouseWork.ViewModels
{
    public class EventDetailsViewModel : INotifyPropertyChanged
    {
        #region Fields

        public event Action RequestClose; // Событие, запрашивающее закрытие окна

        private readonly UnitOfWork _unitOfWork;

        private ObservableCollection<EventsSchedule> _connectedMovies;
        public ObservableCollection<EventsSchedule> ConnectedMovies
        {
            get { return _connectedMovies; }
            set
            {
                _connectedMovies = value;
                OnPropertyChanged(nameof(ConnectedMovies));
            }
        }

        private EventsSchedule _eventDetails;
        public EventsSchedule EventDetails
        {
            get => _eventDetails;
            set
            {
                _eventDetails = value;
                UpdateOrganizerAndCategory();
                OnPropertyChanged(nameof(EventDetails));
            }
        }

        private Organizers _organizer;
        public Organizers Organizer
        {
            get => _organizer;
            set
            {
                _organizer = value;
                OnPropertyChanged(nameof(Organizer));
            }
        }

        private Categories _category;
        public Categories Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private ObservableCollection<Comments> _comments;
        public ObservableCollection<Comments> Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        private string _newCommentText;
        public string NewCommentText
        {
            get => _newCommentText;
            set
            {
                _newCommentText = value;
                OnPropertyChanged(nameof(NewCommentText));
            }
        }

        private int _newRating;
        public int NewRating
        {
            get => _newRating;
            set
            {
                _newRating = value;
                OnPropertyChanged(nameof(NewRating));
            }
        }

        private ObservableCollection<TicketViewModel> _tickets;
        public ObservableCollection<TicketViewModel> Tickets
        {
            get => _tickets;
            set
            {
                _tickets = value;
                OnPropertyChanged(nameof(Tickets));
                OnPropertyChanged(nameof(TicketsByRow));
            }
        }


        private ObservableCollection<TicketViewModel> _selectedTickets;
        public ObservableCollection<TicketViewModel> SelectedTickets
        {
            get => _selectedTickets;
            set
            {
                _selectedTickets = value;
                OnPropertyChanged(nameof(SelectedTickets));
            }
        }

        public ICommand AddCommentCommand { get; }
        public ICommand OpenMovieDetailsCommand { get; }
        public ICommand SelectTicketCommand { get; private set; }
        public ICommand AddToCartCommand { get; private set; }
        public ICommand RemoveFromSelectedCommand { get; private set; }

        public IOrderedEnumerable<IGrouping<int, TicketViewModel>> TicketsByRow =>
            Tickets?.GroupBy(t => t.Row == 0 ? -1 : t.Row).OrderBy(g => g.Key)
            ?? Enumerable.Empty<IGrouping<int, TicketViewModel>>().OrderBy(g => g.Key);

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        private void UpdateOrganizerAndCategory()
        {
            if (_eventDetails != null && _eventDetails.Event != null)
            {
                var organizerID = EventDetails.Event.OrganizerID;
                var categoryID = EventDetails.Event.CategoryID;
                Organizer = _unitOfWork.OrganizerRepository.FindById(organizerID);
                Category = _unitOfWork.CategoryRepository.FindById(categoryID);
            }
        }

        public EventDetailsViewModel(int eventScheduleId)
        {
            _unitOfWork = new UnitOfWork();
            EventDetails = _unitOfWork.EventScheduleRepository.FindById(eventScheduleId);
            Comments = new ObservableCollection<Comments>(_unitOfWork.CommentsRepository.GetCommentsByEventId(EventDetails.EventID));

            Tickets = new ObservableCollection<TicketViewModel>();
            LoadTickets();
            Tickets = new ObservableCollection<TicketViewModel>(Tickets.OrderBy(t => t.Row).ThenBy(t => t.Seat));
            SelectedTickets = new ObservableCollection<TicketViewModel>();

            AddCommentCommand = new RelayCommand(AddComment, CanAddComment);
            OpenMovieDetailsCommand = new RelayCommand(OpenMovieDetails);
            ConnectedMovies = _unitOfWork.EventScheduleRepository.GetConnectedEventSchedule(EventDetails.EventScheduleID);

            SelectTicketCommand = new RelayCommand(SelectTicket);
            AddToCartCommand = new RelayCommand(AddToCart, CanAddToCart);
            RemoveFromSelectedCommand = new RelayCommand(RemoveFromSelected);

            GenerateSeatPlan();
        }
        private void LoadTickets()
        {
            var tickets = _unitOfWork.TicketsRepository.Get(t => t.EventScheduleID == EventDetails.EventScheduleID, includeProperties: "SectorRows");
            Tickets.Clear();
            var groupedTickets = tickets.GroupBy(t => t.SectorRows.SectorRow)
                                        .OrderBy(g => g.Key); 

            foreach (var group in groupedTickets)
            {
                foreach (var ticket in group.OrderBy(t => t.PlaceInRow)) 
                {
                    Tickets.Add(new TicketViewModel(ticket));
                }
            }
        }

        private void GenerateSeatPlan()
        {

            OnPropertyChanged(nameof(Tickets));
        }
        private void SelectTicket(object parameter)
        {
            var ticketVM = parameter as TicketViewModel;
            if (ticketVM == null || ticketVM.Status == "Saled" || ticketVM.Status == "Booked") return;

            if (SelectedTickets.Contains(ticketVM))
            {
                SelectedTickets.Remove(ticketVM);
                ticketVM.IsSelected = false;
            }
            else
            {
                SelectedTickets.Add(ticketVM);
                ticketVM.IsSelected = true;
            }
        }

        private void AddToCart(object parameter)
        {
            var selectedTicketsCopy = new List<TicketViewModel>(SelectedTickets);

            foreach (var ticketVM in selectedTicketsCopy)
            {
                var existingCartItem = _unitOfWork.ShoppingCartRepository.Get(sc => sc.TicketID == ticketVM.Ticket.TicketID).FirstOrDefault();
                if (existingCartItem == null)
                {
                    var newCartItem = new ShoppingCart
                    {
                        ShoppingCartID = _unitOfWork.ShoppingCartRepository.FindMaxId(),
                        UserID = UserSession.CurrentUserID,
                        TicketID = ticketVM.Ticket.TicketID
                    };
                    _unitOfWork.ShoppingCartRepository.Insert(newCartItem);

                    ticketVM.Status = "Booked";
                    var ticket = _unitOfWork.TicketsRepository.GetByID(ticketVM.Ticket.TicketID);
                    ticket.Status = "Booked";
                    _unitOfWork.TicketsRepository.Update(ticket);
                    _unitOfWork.Save();
                }
                else
                {
                    MessageBox.Show($"{(string)Application.Current.Resources["item47"]} : {ticketVM.Ticket.TicketID}");
                }
            }

            LoadTickets();
            SelectedTickets.Clear();
            OnPropertyChanged(nameof(Tickets));
            OnPropertyChanged(nameof(TicketsByRow));

        }


        private bool CanAddToCart(object parameter)
        {
            return SelectedTickets.Any() && _unitOfWork.UserRepository.FindRoleId(UserSession.CurrentUserID) == 1;
        }

        private bool CanAddComment(object parameter)
        {
            var currentEventID = _unitOfWork.EventScheduleRepository
            .GetAll()
            .Where(es => es.EventScheduleID == EventDetails.EventID)
            .Select(es => es.EventID)
            .FirstOrDefault();

            var visitedCount = _unitOfWork.SalesRepository
            .GetAll()
                .Where(sale => sale.UserID == UserSession.CurrentUserID)
                .Join(_unitOfWork.TicketsRepository.GetAll(),
                      sale => sale.TicketID,
                      ticket => ticket.TicketID,
                      (sale, ticket) => new { sale, ticket })
                .Join(_unitOfWork.EventScheduleRepository.GetAll(),
                      saleWithTicket => saleWithTicket.ticket.EventScheduleID,
                      eventSchedule => eventSchedule.EventScheduleID,
                      (saleWithTicket, eventSchedule) => new { saleWithTicket.sale, eventSchedule })
                .Where(saleWithEventSchedule => saleWithEventSchedule.eventSchedule.EventID == EventDetails.EventID
                                                && saleWithEventSchedule.eventSchedule.EventDate < DateTime.Now)
                .Count();
            return visitedCount > 0;
        }

        private void RemoveFromSelected(object parameter)
        {
            var ticketVM = parameter as TicketViewModel;
            if (ticketVM != null)
            {
                SelectedTickets.Remove(ticketVM);
                ticketVM.IsSelected = false;
            }
        }
        public EventDetailsViewModel()
        {

        }
        private void AddComment(object parameter)
        {
            if (NewCommentText.Length > 400)
            {
                MessageBox.Show((string)Application.Current.Resources["item180"]);
                return;
            }

            if (!string.IsNullOrWhiteSpace(NewCommentText) && NewRating > 0)
            {
                var maxID = _unitOfWork.CommentsRepository.FindMaxId();
                var newComment = new Comments
                {
                    CommentID = maxID,
                    UserID = UserSession.CurrentUserID,
                    Login = _unitOfWork.UserRepository.GetByID(UserSession.CurrentUserID).Login,
                    EventID = EventDetails.EventID,
                    CommentText = NewCommentText,
                    CommentDate = DateTime.Now,
                    FivePointRating = NewRating
                };

                _unitOfWork.CommentsRepository.Add(newComment);
                _unitOfWork.Save();

                Comments.Add(newComment);
                NewCommentText = string.Empty;
                NewRating = 0;
            }
        }

        private void OpenMovieDetails(object parameter)
        {
            if (parameter is EventsSchedule schedule)
            {
                var eventScheduleId = schedule.EventScheduleID;
                var currentWindow = Application.Current.Windows.OfType<EventDetails>().FirstOrDefault(w => w.IsActive);

                if (currentWindow != null)
                {
                    currentWindow.DataContext = new EventDetailsViewModel(eventScheduleId);
                }
                else
                {
                    var detailsWindow = new EventDetails
                    {
                        DataContext = new EventDetailsViewModel(eventScheduleId)
                    };
                    detailsWindow.Show();
                }
            }
        }
        #endregion
    }
}
