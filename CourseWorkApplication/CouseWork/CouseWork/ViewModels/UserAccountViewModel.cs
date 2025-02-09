using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace CouseWork.ViewModels
{
    public class UserAccountViewModel : INotifyPropertyChanged
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

        public ObservableCollection<UserQuestions> UserAnswers {get;set;}
        public ObservableCollection<ShoppingCart> CartItems { get; set; }
        public ObservableCollection<CartItem> CartItemDetails { get; set; }
        public ObservableCollection<SalesItem> PurchasedTickets { get; set; }

        public decimal TotalPrice => CartItems.Sum(item => item.Ticket.Price);

        public ICommand RemoveCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand CheckoutCommand { get; }
        public ICommand RefundCommand { get; }
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
        public UserAccountViewModel()
        {
            _unitOfWork = new UnitOfWork();

            CartItems = new ObservableCollection<ShoppingCart>(
                _unitOfWork.ShoppingCartRepository
                    .GetAllByUserId(UserSession.CurrentUserID)
            );

            RemoveCommand = new RelayCommand(RemoveFromCart, CanExecuteItemCommand);

            ClearCommand = new RelayCommand(ClearCart);
            CheckoutCommand = new RelayCommand(Checkout);
            AskCommand = new RelayCommand(AskQuestion, CanAskQuestion);
            RefundCommand = new RelayCommand(RefundTicket, CanRefundTicket);

            CartItemDetails = new ObservableCollection<CartItem>(CartItems.Select(item => new CartItem(item, _unitOfWork)));
            PurchasedTickets = new ObservableCollection<SalesItem>(
            _unitOfWork.SalesRepository
                .GetAllByUserId(UserSession.CurrentUserID)
                .Select(s => new SalesItem(s, _unitOfWork))
            );
            UserAnswers = new ObservableCollection<UserQuestions>(_unitOfWork.UserQuestionRepository.GetUserQuestions(UserSession.CurrentUserID));
        }

        private void RemoveFromCart(object parameter)
        {
            if (parameter is CartItem cartItem && cartItem.ShoppingCart != null)
            {
                var item = cartItem.ShoppingCart;

                var ticket = _unitOfWork.TicketsRepository
                    .GetAll()
                    .FirstOrDefault(t => t.TicketID == item.TicketID);

                if (ticket != null)
                {
                    ticket.Status = "On sale";
                    _unitOfWork.TicketsRepository.Update(ticket);
                }

                _unitOfWork.ShoppingCartRepository.Delete(item.ShoppingCartID);
                _unitOfWork.Save();

                CartItems.Remove(item);
                UpdateCartItemDetails();
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(CartItems));
            }
        }

        private void ClearCart(object parameter)
        {
            var userId = UserSession.CurrentUserID;

            var cartItems = _unitOfWork.ShoppingCartRepository
             .GetAll()
             .Where(cartItem => cartItem.UserID == userId)
             .ToList();

            foreach (var cartItem in cartItems)
            {
                var ticket = _unitOfWork.TicketsRepository
                    .GetAll()
                    .FirstOrDefault(t => t.TicketID == cartItem.TicketID);

                if (ticket != null)
                {
                    ticket.Status = "On sale";
                    _unitOfWork.TicketsRepository.Update(ticket);
                }
            }

            _unitOfWork.ShoppingCartRepository.ClearCartByUserId(userId);
            _unitOfWork.Save();

            CartItems.Clear();
            UpdateCartItemDetails();
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(CartItems));
        }

        private void Checkout(object parameter)
        {
            var userId = UserSession.CurrentUserID;

            foreach (var item in CartItems)
            {
                var ticket = _unitOfWork.TicketsRepository
                    .GetAll()
                    .FirstOrDefault(t => t.TicketID == item.TicketID);

                if (ticket != null)
                {
                    ticket.Status = "Saled";
                    _unitOfWork.TicketsRepository.Update(ticket);
                }

                _unitOfWork.SalesRepository.Add(new Sales
                {
                    SaleID = _unitOfWork.SalesRepository.FindMaxId(),
                    UserID = userId,
                    TicketID = item.TicketID,
                    SaleDate = DateTime.Now,
                    Status = "Actual"
                }) ;
                _unitOfWork.Save();
                UpdatePurchasedTickets();
            }

            _unitOfWork.ShoppingCartRepository.ClearCartByUserId(userId);

            CartItems.Clear();
            _unitOfWork.Save();
            UpdateCartItemDetails();
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(CartItems));
        }

        private void UpdateCartItemDetails()
        {
            CartItemDetails.Clear();
            foreach (var cartItem in CartItems)
            {
                CartItemDetails.Add(new CartItem(cartItem, _unitOfWork));
            }
        }

        private void UpdatePurchasedTickets()
        {
            PurchasedTickets.Clear();
            var refunds = _unitOfWork.SalesRepository
                .GetAllByUserId(UserSession.CurrentUserID)
                .Select(r => new SalesItem(r, _unitOfWork));

            foreach (var refund in refunds)
            {
                PurchasedTickets.Add(refund);
            }
        }


        private bool CanExecuteItemCommand(object parameter)
        {
            var canExecute = parameter is CartItem;
            return canExecute;
        }

        private bool CanAskQuestion(object parameter)
        {
            var canExecute = !string.IsNullOrEmpty(Question);
            return canExecute;
        }

        private void RefundTicket(object parameter)
        {
            if (parameter is SalesItem salesItem)
            {
                var existingRefund = _unitOfWork.TicketRefundRepository
                    .GetAll()
                    .FirstOrDefault(r => r.SaleID == salesItem.SaleID && r.UserID == UserSession.CurrentUserID);

                if (existingRefund != null)
                {
                    MessageBox.Show((string)System.Windows.Application.Current.Resources["item97"]);
                    return;
                }

                var ticketRefund = new TicketRefund
                {
                    TicketRefundID = _unitOfWork.TicketRefundRepository.FindMaxId(),
                    SaleID = salesItem.SaleID,
                    UserID = UserSession.CurrentUserID,
                    RefundDate = DateTime.Now
                };

                _unitOfWork.TicketRefundRepository.Add(ticketRefund);
                _unitOfWork.Save();
                MessageBox.Show((string)System.Windows.Application.Current.Resources["item98"]);

            }
        }

        private void AskQuestion(object parameter)
        {
            _unitOfWork.UserQuestionRepository.AddQuestion(UserSession.CurrentUserID, Question);
            _unitOfWork.Save();
            MessageBox.Show((string)System.Windows.Application.Current.Resources["item99"]);
        }
        private bool CanRefundTicket(object parameter)
        {
            return parameter is SalesItem salesItem && salesItem.Status == "Actual";
        }
        #endregion
    }
    public class CartItem // вспомогательный класс
    {
        public ShoppingCart ShoppingCart { get; }
        public int ShoppingCartID { get; }
        public string EventName { get; }
        public string LocationName { get; }
        public DateTime EventDate { get; }
        public decimal Price { get; }
        public int SectorRow { get; }
        public int PlaceInRow {  get; }
        public CartItem(ShoppingCart cartItem, UnitOfWork _unitOfWork)
        {
            ShoppingCart = cartItem ?? throw new ArgumentNullException(nameof(cartItem));
            var ticket = _unitOfWork.TicketsRepository
                .GetAll()
                .FirstOrDefault(t => t.TicketID == cartItem.TicketID);  

            if (ticket != null)
            {
                var eventSchedule = _unitOfWork.EventScheduleRepository
                    .GetAll()
                    .FirstOrDefault(es => es.EventScheduleID == ticket.EventScheduleID);

                var sectorNumber = _unitOfWork.SectorRowRepository.FindById(ticket.SectorRowID);

                if (eventSchedule != null)
                {
                    var eventItem = _unitOfWork.EventRepository
                        .GetAll()
                        .FirstOrDefault(e => e.EventID == eventSchedule.EventID);  

                    var location = _unitOfWork.LocationRepository
                        .GetAll()
                        .FirstOrDefault(l => l.LocationID == eventSchedule.LocationID);  

                    EventName = eventItem?.EventName ?? (string)System.Windows.Application.Current.Resources["item96"];
                    LocationName = location?.LocationName ?? (string)System.Windows.Application.Current.Resources["item96"];
                    EventDate = eventSchedule?.EventDate ?? DateTime.MinValue;
                    Price = ticket.Price;
                    PlaceInRow = ticket.PlaceInRow;
                    SectorRow = sectorNumber.SectorRow;
                }
                else
                {
                    EventName = (string)System.Windows.Application.Current.Resources["item96"];
                    LocationName = (string)System.Windows.Application.Current.Resources["item96"];
                    EventDate = DateTime.MinValue;
                    Price = 0;
                }

                ShoppingCartID = cartItem.ShoppingCartID;
            }
            else
            {
                EventName = (string)System.Windows.Application.Current.Resources["item96"];
                LocationName = (string)System.Windows.Application.Current.Resources["item96"];
                EventDate = DateTime.MinValue;
                Price = 0;
            }
        }

    }
    public class SalesItem // вспомогательный класс
    {
        public int SaleID { get; set; }
        public string EventName { get; set; }
        public string LocationName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int SectorRow { get; }
        public int PlaceInRow { get; }

        public SalesItem(Sales sale, UnitOfWork unitOfWork)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));

            SaleID = sale.SaleID;
            Status = sale.Status;

            var ticket = unitOfWork.TicketsRepository
                .GetAll()
                .FirstOrDefault(t => t.TicketID == sale.TicketID);

            if (ticket != null)
            {
                var eventSchedule = unitOfWork.EventScheduleRepository
                    .GetAll()
                    .FirstOrDefault(es => es.EventScheduleID == ticket.EventScheduleID);

                var sectorNumber = unitOfWork.SectorRowRepository.FindById(ticket.SectorRowID);

                if (eventSchedule != null)
                {
                    var eventItem = unitOfWork.EventRepository
                        .GetAll()
                        .FirstOrDefault(e => e.EventID == eventSchedule.EventID);

                    var location = unitOfWork.LocationRepository
                        .GetAll()
                        .FirstOrDefault(l => l.LocationID == eventSchedule.LocationID);

                    EventName = eventItem?.EventName ?? (string)System.Windows.Application.Current.Resources["item96"];
                    LocationName = location?.LocationName ?? (string)System.Windows.Application.Current.Resources["item96"];
                    EventDate = eventSchedule?.EventDate ?? DateTime.MinValue;
                    Price = ticket.Price;
                    PlaceInRow = ticket.PlaceInRow;
                    SectorRow = sectorNumber.SectorRow;
                }
                else
                {
                    EventName = (string)System.Windows.Application.Current.Resources["item96"];
                    LocationName = (string)System.Windows.Application.Current.Resources["item96"];
                    EventDate = DateTime.MinValue;
                    Price = 0;
                }
            }
            else
            {
                EventName = (string)System.Windows.Application.Current.Resources["item96"];
                LocationName = (string)System.Windows.Application.Current.Resources["item96"];
                EventDate = DateTime.MinValue;
                Price = 0;
            }
        }
    }


}
