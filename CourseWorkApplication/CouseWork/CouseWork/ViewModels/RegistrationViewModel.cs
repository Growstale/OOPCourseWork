using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CouseWork.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        #region Fields

        UnitOfWork unitOfWork;

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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                PasswordError = string.Empty;
            }
        }

        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
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
        private bool _isOrganizerRegistration = false;
        public bool IsOrganizerRegistration
        {
            get => _isOrganizerRegistration;
            set
            {
                _isOrganizerRegistration = value;
                OnPropertyChanged(nameof(IsOrganizerRegistration));
                OnPropertyChanged(nameof(LoginLabel));
                OnPropertyChanged(nameof(RegistrText));

            }
        }
        public string LoginLabel
        {
            get
            {

                return IsOrganizerRegistration ? (string)Application.Current.Resources["item13"] : (string)Application.Current.Resources["item14"];
            }
        }
        public string RegistrText
        {
            get
            {
                return IsOrganizerRegistration ? (string)Application.Current.Resources["item15"] : (string)Application.Current.Resources["item16"];
            }
        }

        public ICommand SigninCommand { get; }
        public ICommand ToggleRegistrationTypeCommand { get; }
        public ICommand LoginCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        public RegistrationViewModel()
        {
            unitOfWork = new UnitOfWork();
            SigninCommand = new RelayCommand(ExecuteSingin, CanExecute);
            ToggleRegistrationTypeCommand = new RelayCommand(ToggleRegistrationType, CanExecute);

            LocalizationManager.Instance.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(LocalizationManager.CurrentCulture))
                {
                    OnPropertyChanged(nameof(LoginLabel));
                    OnPropertyChanged(nameof(RegistrText));
                    ValidateFields();
               }
            };

        }

        private void ResetErrorFields()
        {
            LoginError = string.Empty;
            PasswordError = string.Empty;
            FirstNameError = string.Empty;
            SecondNameError = string.Empty;
            EmailError = string.Empty;
            PhoneError = string.Empty;
        }


        private bool CanExecute(object parameter)
        {
            return true;
        }

        private void ExecuteSingin(object parameter)
        {
            bool isValid = ValidateFields();

            if (isValid)
            {
                if(unitOfWork.UserRepository.AddNewUser(Login, Password, FirstName, SecondName, Email, Phone, IsOrganizerRegistration))
                {
                    MessageBox.Show((string)Application.Current.Resources["item17"]);
                }
                else
                {
                    MessageBox.Show((string)Application.Current.Resources["item18"]);
                }
            }
        }
        private void ToggleRegistrationType(object parameter)
        {
            IsOrganizerRegistration = !IsOrganizerRegistration;
        }

        private bool ValidateFields()
        {
            ResetErrorFields();
            bool isValid = true;

            if (unitOfWork.UserRepository.CheckLoginExists(Login))
            {
                LoginError = (string)Application.Current.Resources["item19"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Login))
            {
                LoginError = (string)Application.Current.Resources["item87"]; 
                isValid = false;
            }
            else if (Login.Length < 4 && Login.Length > 40)
            {
                LoginError = (string)Application.Current.Resources["item88"]; 
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = (string)Application.Current.Resources["item22"]; 
                isValid = false;
            }
            else if (!ValidatePassword(Password))
            {
                PasswordError = (string)Application.Current.Resources["item23"]; 
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
            else if (FirstName.Length > 20)
            {
                FirstNameError = (string)Application.Current.Resources["item82"];
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
            else if (SecondName.Length > 25)
            {
                SecondNameError = (string)Application.Current.Resources["item83"];
                isValid = false;
            }

            if (unitOfWork.UserRepository.CheckEmailExists(Email))
            {
                EmailError = (string)Application.Current.Resources["item28"];
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
            else if (Email.Length > 50)
            {
                EmailError = (string)Application.Current.Resources["item84"];
                isValid = false;
            }

            if (unitOfWork.UserRepository.CheckPhoneExists(Phone))
            {
                PhoneError = (string)Application.Current.Resources["item31"];
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
                   password.Length <= 20 &&
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
        public void ResetError()
        {
            LoginError = (string)Application.Current.Resources["item19"];
            PasswordError = (string)Application.Current.Resources["item22"];
            FirstNameError = (string)Application.Current.Resources["item24"];
            SecondNameError = (string)Application.Current.Resources["item26"];
            EmailError = (string)Application.Current.Resources["item28"];
            PhoneError = (string)Application.Current.Resources["item31"];
        }
        #endregion
    }
}
