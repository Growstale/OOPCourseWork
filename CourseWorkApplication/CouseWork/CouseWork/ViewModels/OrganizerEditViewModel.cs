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
using System.Windows;

namespace CouseWork.ViewModels
{
    public class OrganizerEditViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly UnitOfWork _unitOfWork;

        private ObservableCollection<Organizers> _organizers;
        public ObservableCollection<Organizers> Organizers
        {
            get => _organizers;
            set
            {
                _organizers = value;
                OnPropertyChanged(nameof(Organizers));
            }
        }
        private Organizers _selectedOrganizer;
        public Organizers SelectedOrganizer
        {
            get => _selectedOrganizer;
            set
            {
                _selectedOrganizer = value;
                OnPropertyChanged(nameof(SelectedOrganizer));

                if (value != null)
                {
                    OrganizerID = value.OrganizerID.ToString();
                    CompanyName = value.CompanyName;
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

        private string _organizerID;
        public string OrganizerID
        {
            get => _organizerID;
            set
            {
                _organizerID = value;
                OnPropertyChanged(nameof(OrganizerID));
                OrganizerIDError = string.Empty;
            }
        }

        private string _organizerIDError;
        public string OrganizerIDError
        {
            get => _organizerIDError;
            set
            {
                _organizerIDError = value;
                OnPropertyChanged(nameof(OrganizerIDError));
            }
        }


        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
                CompanyNameError = string.Empty;
            }
        }

        private string _companyNameError;
        public string CompanyNameError
        {
            get => _companyNameError;
            set
            {
                _companyNameError = value;
                OnPropertyChanged(nameof(CompanyNameError));
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
        public OrganizerEditViewModel()
        {
            _unitOfWork = new UnitOfWork();

            Organizers = _unitOfWork.OrganizerRepository.GetOrganizers();

            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void ResetFields()
        {
            OrganizerID = string.Empty;
            CompanyName = string.Empty;
            FirstName = string.Empty;
            SecondName = null;
            Email = string.Empty;
            Phone = string.Empty;
        }

        private void SaveChanges(object parameter)
        {
            if (ValidateFields())
            {
                var updatedUser = _unitOfWork.UserRepository.UpdateUser(
                    int.Parse(OrganizerID),
                    CompanyName,
                    FirstName,
                    SecondName,
                    Email,
                    Phone
                );

                if (updatedUser != null) RefreshUsers();
            }
        }

        private void ResetErrorFields()
        {
            OrganizerIDError = string.Empty;
            CompanyNameError = string.Empty;
            EmailError = string.Empty;
            PhoneError = string.Empty;
            FirstNameError = string.Empty;
            SecondNameError = string.Empty;
        }

        private void RefreshUsers()
        {
            Organizers = _unitOfWork.OrganizerRepository.GetOrganizers();
        }

        private bool ValidateFields()
        {
            ResetErrorFields();
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(OrganizerID))
            {
                OrganizerIDError = (string)Application.Current.Resources["item80"];
                isValid = false;
                return isValid;
            }


            if (int.TryParse(OrganizerID, out int result))
            {
                if (_unitOfWork.UserRepository.CheckLoginExistsExceptCurrent(CompanyName, result))
                {
                    CompanyNameError = (string)Application.Current.Resources["item19"];
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
                OrganizerIDError = (string)Application.Current.Resources["item80"];
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(CompanyName) && (CompanyName.Length < 4 || CompanyName.Length > 40))
            {
                CompanyNameError = (string)Application.Current.Resources["item21"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                CompanyNameError = (string)Application.Current.Resources["item20"];
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
