using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace CouseWork.ViewModels
{
    public class UserEditViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly UnitOfWork _unitOfWork;

        private ObservableCollection<Users> _users;
        public ObservableCollection<Users> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        private Users _selectedUser;
        public Users SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));

                if (value != null)
                {
                    UserID = value.UserID.ToString();
                    Login = value.Login;
                    FirstName = value.FirstName;
                    SecondName = value.LastName;
                    Email = value.Email;
                    Phone = value.Phone;
                }
                else
                {
                    ResetFields();
                }
            }

        }

        private string _userID;
        public string UserID
        {
            get => _userID;
            set
            {
                _userID = value;
                OnPropertyChanged(nameof(UserID));
                UserIDError = string.Empty;
            }
        }

        private string _userIDError;
        public string UserIDError
        {
            get => _userIDError;
            set
            {
                _userIDError = value;
                OnPropertyChanged(nameof(UserIDError));
            }
        }


        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                LoginError = string.Empty;
            }
        }

        private string _loginError;
        public string LoginError
        {
            get => _loginError;
            set
            {
                _loginError = value;
                OnPropertyChanged(nameof(LoginError));
            }
        }

        
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                FirstNameError = string.Empty;
            }
        }

        private string _firstNameError;
        public string FirstNameError
        {
            get => _firstNameError;
            set
            {
                _firstNameError = value;
                OnPropertyChanged(nameof(FirstNameError));
            }
        }

        private string _secondName;
        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged(nameof(SecondName));
                SecondNameError = string.Empty;
            }
        }

        private string _secondNameError;
        public string SecondNameError
        {
            get => _secondNameError;
            set
            {
                _secondNameError = value;
                OnPropertyChanged(nameof(SecondNameError));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                EmailError = string.Empty;
            }
        }

        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
                PhoneError = string.Empty;
            }
        }

        private string _phoneError;
        public string PhoneError
        {
            get => _phoneError;
            set
            {
                _phoneError = value;
                OnPropertyChanged(nameof(PhoneError));
            }
        }
        public ICommand SaveCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods
        public UserEditViewModel()
        {
            _unitOfWork = new UnitOfWork();

            Users = _unitOfWork.UserRepository.GetUsers();

            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void ResetFields()
        {
            UserID = string.Empty;
            Login = string.Empty;
            FirstName = string.Empty;
            SecondName = null;
            Email = string.Empty;
            Phone = string.Empty;
        }
        private void ResetErrorFields()
        {
            UserIDError = string.Empty;
            LoginError = string.Empty;
            EmailError = string.Empty;
            PhoneError = string.Empty;
            FirstNameError = string.Empty;
            SecondNameError = string.Empty;
        }


        private void SaveChanges(object parameter)
        {
            if (ValidateFields())
            {
                var updatedUser = _unitOfWork.UserRepository.UpdateUser(
                    int.Parse(UserID),
                    Login,
                    FirstName,
                    SecondName,
                    Email,
                    Phone
                );

                if (updatedUser != null) RefreshUsers();                
            }
        }

        private void RefreshUsers()
        {
            Users = _unitOfWork.UserRepository.GetUsers();
        }



        private bool ValidateFields()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(UserID))
            {
                UserIDError = (string)Application.Current.Resources["item100"];
                isValid = false;
                return isValid;
            }

            if (int.TryParse(UserID, out int result))
            {
                if (_unitOfWork.UserRepository.CheckLoginExistsExceptCurrent(Login, result))
                {
                    LoginError = (string)Application.Current.Resources["item19"];
                    isValid = false;
                }
                if (_unitOfWork.UserRepository.CheckEmailExistsExceptCurrent(Email, result))
                {
                    EmailError = (string)Application.Current.Resources["item28"];
                    isValid = false;
                }
                if (_unitOfWork.UserRepository.CheckPhoneExistsExceptCurrent(Phone, result))
                {
                    PhoneError = (string)Application.Current.Resources["item31"];
                    isValid = false;
                }
            }
            else
            {
                UserIDError = (string)Application.Current.Resources["item100"];
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(Login) && (Login.Length < 4 || Login.Length > 40))
            {
                LoginError = (string)Application.Current.Resources["item88"];
                isValid = false;
            }
            
            if (string.IsNullOrWhiteSpace(Login))
            {
                LoginError = (string)Application.Current.Resources["item87"];
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(FirstName) && FirstName.Length > 20)
            {
                FirstNameError = (string)Application.Current.Resources["item82"];
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                FirstNameError = (string)Application.Current.Resources["item24"];
                isValid = false;
            }
            else if (!ValidateLettersOnly(FirstName))
            {
                FirstNameError = (string)Application.Current.Resources["item25"];
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(SecondName) && SecondName.Length > 25)
            {
                SecondNameError = (string)Application.Current.Resources["item83"];
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(SecondName))
            {
                SecondNameError = (string)Application.Current.Resources["item26"];
                isValid = false;
            }
            else if (!ValidateLettersOnly(SecondName))
            {
                SecondNameError = (string)Application.Current.Resources["item27"];
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(Email) && Email.Length > 50)
            {
                EmailError = (string)Application.Current.Resources["item84"];
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = (string)Application.Current.Resources["item29"];
                isValid = false;
            }
            else if (!ValidateEmail(Email))
            {
                EmailError = (string)Application.Current.Resources["item30"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Phone))
            {
                PhoneError = (string)Application.Current.Resources["item32"];
                isValid = false;
            }
            else if (!ValidatePhone(Phone))
            {
                PhoneError = (string)Application.Current.Resources["item33"];
                isValid = false;
            }

            return isValid;
        }

        private bool ValidatePassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => "!@#$%^&*()_+=-.".Contains(ch));
        }

        private bool ValidateLettersOnly(string input)
        {
            return input.All(char.IsLetter);
        }

        private bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidatePhone(string phone)
        {
            return phone.All(char.IsDigit) && phone.Length >= 10 && phone.Length <= 15;
        }

        #endregion
    }
}
