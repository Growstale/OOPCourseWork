using CouseWork.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CouseWork.ViewModels
{
    public class TicketViewModel : INotifyPropertyChanged // вспомогательный viewmodel для eventdetailviewmodel
    {
        #region Fields
        public int Sector => Ticket.SectorRows.SectorRow;
        public int Row => Ticket.SectorRows.SectorRow;
        public int Seat => Ticket.PlaceInRow;
        public decimal Price => Ticket.Price;

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
                SeatColor = CalculateSeatColor();
            }
        }


        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                SeatColor = CalculateSeatColor();
            }
        }

        private Color _seatColor = Colors.Transparent;
        public Color SeatColor
        {
            get
            {
                return _seatColor == Colors.Transparent ? CalculateSeatColor() : _seatColor;
            }
            private set
            {
                _seatColor = value;
                OnPropertyChanged(nameof(SeatColor));
            }
        }
        public Tickets Ticket { get; private set; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        public TicketViewModel(Tickets ticket)
        {
            Ticket = ticket;
            _status = ticket.Status;
            _isSelected = false;
        }

        private Color CalculateSeatColor()
        {

            if (IsSelected) return Colors.Orange;
            return Status switch
            {
                "On sale" => Colors.LightGreen,
                "Booked" => Colors.Yellow,
                "Saled" => Colors.Red,
                _ => Colors.Gray
            };
        }
        #endregion
    }
}
