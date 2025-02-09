using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;

namespace CouseWork.ViewModels
{
    public class LocationEditViewModel : INotifyPropertyChanged
    {
        #region Fields
        UnitOfWork unitOfWork;
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }


        private ObservableCollection<Locations> _locations;

        public ObservableCollection<Locations> Locations
        {
            get => _locations;
            set
            {
                if (_locations != value)
                {
                    _locations = value;
                    OnPropertyChanged(nameof(Locations));
                }
            }
        }
        private Locations _selectedLocation;
        public Locations SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));

                if (value != null)
                {
                    Id = value.LocationID.ToString();
                    Name = value.LocationName;
                    Rows = value.NumberOfSectors.ToString();
                }
                else
                {
                    Id = string.Empty;
                    Name = string.Empty;
                    Rows = string.Empty;
                }
            }
        }

        public string _id;
        public string _name;
        public string _rows;
        public string _idError;
        public string _nameError;
        public string _rowsError;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
                IdError = string.Empty;

            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                NameError = string.Empty;
            }
        }

        public string Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                OnPropertyChanged(nameof(Rows));
                RowsError = string.Empty;
            }
        }
        public string IdError
        {
            get { return _idError; }
            set
            {
                _idError = value;
                OnPropertyChanged(nameof(IdError));
            }
        }

        public string NameError
        {
            get { return _nameError; }
            set
            {
                _nameError = value;
                OnPropertyChanged(nameof(NameError));
            }
        }

        public string RowsError
        {
            get { return _rowsError; }
            set
            {
                _rowsError = value;
                OnPropertyChanged(nameof(RowsError));
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods
        public LocationEditViewModel()
        {
            unitOfWork = new UnitOfWork();
            Locations = unitOfWork.LocationRepository.GetLocations();
            SaveCommand = new RelayCommand(SaveChanges);
            AddCommand = new RelayCommand(AddLocation);
            DeleteCommand = new RelayCommand(DeleteLocation);

        }

        private void ResetErrorFields()
        {
            IdError = string.Empty;
            NameError = string.Empty;
            RowsError = string.Empty;
        }

        private void SaveChanges(object parameter)
        {
            if (ValidateFieldsUdpate())
            {
                if (SelectedLocation != null)
                {
                    var newLocation = unitOfWork.LocationRepository.UpdateLocation(SelectedLocation.LocationID, Name, Rows);
                    if (newLocation != null)
                    {
                        var index = Locations.IndexOf(SelectedLocation);
                        Locations.RemoveAt(index);
                        Locations.Insert(index, newLocation);
                    }
                }
            }
        }
        private void AddLocation(object parameter)
        {
            if (ValidateFieldsAdd())
            {
                var newLocation = unitOfWork.LocationRepository.AddLocation(Name, Rows);
                if (newLocation != null) Locations.Add(newLocation);
            }
        }
        private void DeleteLocation(object parameter)
        {
            if (ValidateFieldsDelete())
            {
                if (SelectedLocation != null)
                {
                    var newLocation = unitOfWork.LocationRepository.DeleteLocation(SelectedLocation.LocationID);
                    if (newLocation != null) Locations.Remove(newLocation);
                }
            }
        }
        private bool ValidateFieldsUdpate()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Id))
            {
                IdError = (string)System.Windows.Application.Current.Resources["item61"];
                isValid = false;
                return isValid;
            }

            if (!string.IsNullOrWhiteSpace(Name) && Name.Length > 60)
            {
                NameError = (string)System.Windows.Application.Current.Resources["item72"];
                isValid = false;
            }


            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = (string)System.Windows.Application.Current.Resources["item39"];
                isValid = false;
            }

            if (int.TryParse(Rows, out int x) && (x < 0 || x > 150))
            {
                RowsError = (string)System.Windows.Application.Current.Resources["item73"];
                isValid = false;
            }

            if (!int.TryParse(Rows, out int _))
            {
                RowsError = (string)System.Windows.Application.Current.Resources["item74"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Rows))
            {
                RowsError = (string)System.Windows.Application.Current.Resources["item79"];
                isValid = false;
            }

            return isValid;
        }
        private bool ValidateFieldsAdd()
        {
            ResetErrorFields();
            bool isValid = true;

            if (unitOfWork.LocationRepository.CheckLocationUnique(Name))
            {
                NameError = (string)System.Windows.Application.Current.Resources["item75"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = (string)System.Windows.Application.Current.Resources["item39"];
                isValid = false;
            }


            if (int.TryParse(Rows, out int x) && (x < 0 || x > 150))
            {
                RowsError = (string)System.Windows.Application.Current.Resources["item73"];
                isValid = false;
            }

            if (!int.TryParse(Rows, out int _))
            {
                RowsError = (string)System.Windows.Application.Current.Resources["item74"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Rows))
            {
                RowsError = (string)System.Windows.Application.Current.Resources["item79"];
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateFieldsDelete()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Id))
            {
                IdError = (string)System.Windows.Application.Current.Resources["item61"];
                isValid = false;
            }

            return isValid;
        }
        #endregion
    }
}
