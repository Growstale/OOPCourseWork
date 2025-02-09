using CouseWork.Commands;
using CouseWork.Data.Repositories;
using CouseWork.Data;
using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace CouseWork.ViewModels
{
    class TicketRefundViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly UnitOfWork _unitOfWork;

        private ObservableCollection<TicketRefund> _ticketRefunds;
        public ObservableCollection<TicketRefund> TicketRefunds
        {
            get => _ticketRefunds;
            set
            {
                _ticketRefunds = value;
                OnPropertyChanged(nameof(TicketRefunds));
            }
        }

        private TicketRefund _selectedTicketRefund;
        public TicketRefund SelectedTicketRefund
        {
            get => _selectedTicketRefund;
            set
            {
                _selectedTicketRefund = value;
                OnPropertyChanged(nameof(SelectedTicketRefund));
                if (value != null)
                {
                    TicketRefundID = value.TicketRefundID.ToString();
                    SaleID = value.SaleID.ToString();
                    UserName = _unitOfWork.UserRepository.GetByID(value.UserID).Login;
                    RefundDate = value.RefundDate.ToString();
                }
            }
        }

        private string _ticketRefundID;
        public string TicketRefundID
        {
            get { return _ticketRefundID; }
            set
            {
                _ticketRefundID = value;
                OnPropertyChanged(nameof(TicketRefundID));
            }
        }

        private string _saleID;
        public string SaleID
        {
            get { return _saleID; }
            set
            {
                _saleID = value;
                OnPropertyChanged(nameof(SaleID));
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _refundDate;
        public string RefundDate
        {
            get { return _refundDate; }
            set
            {
                _refundDate = value;
                OnPropertyChanged(nameof(RefundDate));
            }
        }

        public ICommand ApproveRefundCommand { get; }
        public ICommand RejectRefundCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        public TicketRefundViewModel()
        {
            _unitOfWork = new UnitOfWork();
            ApproveRefundCommand = new RelayCommand(ApproveRefund);
            RejectRefundCommand = new RelayCommand(RejectRefund);
            TicketRefunds = new ObservableCollection<TicketRefund>(_unitOfWork.TicketRefundRepository.GetAll());

        }

        private void ReloadTicketRefunds()
        {
            TicketRefunds = new ObservableCollection<TicketRefund>(_unitOfWork.TicketRefundRepository.GetAll());
            OnPropertyChanged(nameof(TicketRefunds));
        }

        private void ApproveRefund(object options)
        {

            MessageBox.Show(TicketRefunds.Count.ToString());
            if (SelectedTicketRefund == null) return;

            var sale = _unitOfWork.SalesRepository.GetById(SelectedTicketRefund.SaleID);
            if (sale != null)
            {
                sale.Status = "Refund";
                _unitOfWork.Save();
            }

            var ticket = _unitOfWork.TicketsRepository.GetByID(sale.TicketID);
            if (ticket != null)
            {
                ticket.Status = "On sale";
                _unitOfWork.Save();
            }

            _unitOfWork.TicketRefundRepository.Delete(_unitOfWork.TicketRefundRepository.GetByID(SelectedTicketRefund.TicketRefundID));
            _unitOfWork.Save();
            ReloadTicketRefunds();
        }

        private void RejectRefund(object options)
        {
            if (SelectedTicketRefund == null) return;

            _unitOfWork.TicketRefundRepository.Delete(_unitOfWork.TicketRefundRepository.GetByID(SelectedTicketRefund.TicketRefundID));
            _unitOfWork.Save();
            ReloadTicketRefunds();
        }
        #endregion
    }
}