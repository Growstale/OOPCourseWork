using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CouseWork.Commands;
using CouseWork.Data;
using System.Configuration;
using CouseWork.Views;
using CouseWork.Utilities;

namespace CouseWork.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        #region Fields

        UnitOfWork unitOfWork;
        public string _username;
        public string _password;
        public string _usernameError;
        public string _passwordError;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                UsernameError = string.Empty;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                PasswordError = string.Empty;
            }
        }

        public string UsernameError
        {
            get { return _usernameError; }
            set
            {
                _usernameError = value;
                OnPropertyChanged(nameof(UsernameError));
            }
        }

        public string PasswordError
        {
            get { return _passwordError; }
            set
            {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }
        public ICommand LoginCommand { get; }

        public static int SubscriberCount { get; private set; } = 0;
        private static event Action _requestClose;

        public static event Action RequestClose
        {
            add
            {
                _requestClose += value;
                SubscriberCount++;
            }
            remove
            {
                _requestClose -= value;
                SubscriberCount--;
            }
        }


        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        public AuthorizationViewModel()
        {
            unitOfWork = new UnitOfWork();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            LocalizationManager.Instance.PropertyChanged += (sender, args) =>
            {
                RefreshErrorMessages();
            };

        }
        private void RefreshErrorMessages()
        {
            if (!string.IsNullOrEmpty(UsernameError)) UsernameError = (string)Application.Current.Resources["item5"];
            if (!string.IsNullOrEmpty(PasswordError)) PasswordError = (string)Application.Current.Resources["item6"];
        }  

        private bool CanExecuteLogin(object parameter)
        {
            return true;
        }

        private void ExecuteLogin(object parameter)
        {
            bool isValid = ValidateFields();

            if (isValid)
            {
                bool result = unitOfWork.UserRepository.AuthorizeUser(Username, Password);
                if (result)
                {
                    _requestClose?.Invoke();
                }
                else MessageBox.Show((string)Application.Current.Resources["item4"]);
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Username))
            {
                UsernameError = (string)Application.Current.Resources["item5"];
                isValid = false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                PasswordError = (string)Application.Current.Resources["item6"];
                isValid = false;
            }

            return isValid;
        }
        #endregion
    }
}
