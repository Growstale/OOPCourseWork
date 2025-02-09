using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CouseWork.ViewModels
{
    public class SectorRowEditViewModel : INotifyPropertyChanged
    {
        #region Fields
        UnitOfWork unitOfWork;

        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        private ObservableCollection<SectorRows> _sectorRows;
        public ObservableCollection<SectorRows> SectorRows
        {
            get => _sectorRows;
            set
            {
                if (_sectorRows != value)
                {
                    _sectorRows = value;
                    OnPropertyChanged(nameof(SectorRows));
                }
            }
        }

        private SectorRows _selectedSectorRow;
        public SectorRows SelectedSectorRow
        {
            get => _selectedSectorRow;
            set
            {
                _selectedSectorRow = value;
                OnPropertyChanged(nameof(SelectedSectorRow));

                if (value != null)
                {
                    Id = value.SectorRowID.ToString();
                    SectorRow = value.SectorRow.ToString();
                    NumberOfSeats = value.NumberOfSeats.ToString();
                    SelectedLocation = value.Location;
                    CostFactor = value.CostFactor.ToString();
                }
                else
                {
                    Id = string.Empty;
                    SectorRow = string.Empty;
                    NumberOfSeats = string.Empty;
                    SelectedLocation = null;
                    CostFactor = string.Empty;
                }
            }
        }

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
            }
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                    IdError = string.Empty;
                }
            }
        }

        private string _sectorRow;
        public string SectorRow
        {
            get => _sectorRow;
            set
            {
                if (_sectorRow != value)
                {
                    _sectorRow = value;
                    OnPropertyChanged(nameof(SectorRow));
                    SectorRowError = string.Empty;
                }
            }
        }

        private string _numberOfSeats;
        public string NumberOfSeats
        {
            get => _numberOfSeats;
            set
            {
                if (_numberOfSeats != value)
                {
                    _numberOfSeats = value;
                    OnPropertyChanged(nameof(NumberOfSeats));
                    NumberOfSeatsError = string.Empty;
                }
            }
        }

        private string _costFactor;
        public string CostFactor
        {
            get => _costFactor;
            set
            {
                if (_costFactor != value)
                {
                    _costFactor = value;
                    OnPropertyChanged(nameof(CostFactor));
                    CostFactorError = string.Empty;
                }
            }
        }

        private string _idError;
        public string IdError
        {
            get => _idError;
            set
            {
                if (_idError != value)
                {
                    _idError = value;
                    OnPropertyChanged(nameof(IdError));
                }
            }
        }

        private string _sectorRowError;
        public string SectorRowError
        {
            get => _sectorRowError;
            set
            {
                if (_sectorRowError != value)
                {
                    _sectorRowError = value;
                    OnPropertyChanged(nameof(SectorRowError));
                }
            }
        }

        private string _numberOfSeatsError;
        public string NumberOfSeatsError
        {
            get => _numberOfSeatsError;
            set
            {
                if (_numberOfSeatsError != value)
                {
                    _numberOfSeatsError = value;
                    OnPropertyChanged(nameof(NumberOfSeatsError));
                }
            }
        }

        private string _costFactorError;
        public string CostFactorError
        {
            get => _costFactorError;
            set
            {
                if (_costFactorError != value)
                {
                    _costFactorError = value;
                    OnPropertyChanged(nameof(CostFactorError));
                }
            }
        }

        private string _locationError;
        public string LocationError
        {
            get => _locationError;
            set
            {
                if (_locationError != value)
                {
                    _locationError = value;
                    OnPropertyChanged(nameof(LocationError));
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
        public SectorRowEditViewModel()
        {
            unitOfWork = new UnitOfWork();
            Locations = new ObservableCollection<Locations>(unitOfWork.LocationRepository.GetLocations());
            SectorRows = new ObservableCollection<SectorRows>(unitOfWork.SectorRowRepository.GetSectorRows());
            SaveCommand = new RelayCommand(Save);
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Add(object param)
        {
            if (ValidateAdd())
            {
                var sectorRowEntity = unitOfWork.SectorRowRepository.AddSectorRow(int.Parse(NumberOfSeats), SelectedLocation.LocationID, decimal.Parse(CostFactor));
                if (sectorRowEntity != null) SectorRows.Add(sectorRowEntity);
                SectorRows.Clear();
                SectorRows = new ObservableCollection<SectorRows>(unitOfWork.SectorRowRepository.GetSectorRows());
            }
        }

        private void Save(object param)
        {
            if (SelectedSectorRow != null)
            {
                if (ValidateUpdate())
                {
                    var sectorRowEntity = unitOfWork.SectorRowRepository.UpdateSectorRow(SelectedSectorRow.SectorRowID, int.Parse(NumberOfSeats), decimal.Parse(CostFactor));
                    if (sectorRowEntity != null)
                    {
                        var index = SectorRows.IndexOf(SelectedSectorRow);
                        SectorRows.RemoveAt(index);
                        SectorRows.Insert(index, sectorRowEntity);
                    }
                    SectorRows.Clear();
                    SectorRows = new ObservableCollection<SectorRows>(unitOfWork.SectorRowRepository.GetSectorRows());
                }
            }
        }

        private void Delete(object param)
        {
            if (SelectedSectorRow != null)
            {
                if (ValidateDelete())
                {

                    var sectorRowEntity = unitOfWork.SectorRowRepository.DeleteSectorRow(SelectedSectorRow.SectorRowID);
                    if (sectorRowEntity != null)
                    {
                        SectorRows.Remove(sectorRowEntity);
                        Locations.Clear();
                        Locations = new ObservableCollection<Locations>(unitOfWork.LocationRepository.GetLocations());
                        SectorRows.Clear();
                        SectorRows = new ObservableCollection<SectorRows>(unitOfWork.SectorRowRepository.GetSectorRows());
                    }
                }
            }        
        }

        private void ResetErrorFields()
        {
            IdError = string.Empty;
            NumberOfSeatsError = string.Empty;
            CostFactorError = string.Empty;
            SectorRowError = string.Empty;
            LocationError = string.Empty;
        }


        private bool ValidateDelete()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Id))
            {
                IdError = "Выберите ряд";
                isValid = false;
            }

            return isValid;
        }
        private bool ValidateUpdate()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Id))
            {
                IdError = (string)System.Windows.Application.Current.Resources["item95"];
                isValid = false;
                return isValid;
            }


            if (int.TryParse(NumberOfSeats, out int x) && (x < 0 || x > 200))
            {
                NumberOfSeatsError = (string)System.Windows.Application.Current.Resources["item89"];
                isValid = false;
            }

            if (!int.TryParse(NumberOfSeats, out int _))
            {
                NumberOfSeatsError = (string)System.Windows.Application.Current.Resources["item90"];
                isValid = false;
            }

            if (decimal.TryParse(CostFactor, out decimal y) && (y < 1 || y > 50))
            {
                CostFactorError = (string)System.Windows.Application.Current.Resources["item91"];
                isValid = false;
            }

            if (!decimal.TryParse(CostFactor, out decimal _))
            {
                CostFactorError = (string)System.Windows.Application.Current.Resources["item92"];
                isValid = false;
            }
            if (SelectedLocation.LocationID != unitOfWork.SectorRowRepository.FindById(int.Parse(Id)).LocationID)
            {
                LocationError = (string)System.Windows.Application.Current.Resources["item93"];
                isValid = false;
            }
            return isValid;
        }
        private bool ValidateAdd()
        {
            ResetErrorFields();
            bool isValid = true;

            if (int.TryParse(NumberOfSeats, out int x) && (x < 0 || x > 200))
            {
                NumberOfSeatsError = (string)System.Windows.Application.Current.Resources["item89"];
                isValid = false;
            }

            if (!int.TryParse(NumberOfSeats, out int _))
            {
                NumberOfSeatsError = (string)System.Windows.Application.Current.Resources["item90"];
                isValid = false;
            }

            if (decimal.TryParse(CostFactor, out decimal y) && (y < 1 || y > 50))
            {
                CostFactorError = (string)System.Windows.Application.Current.Resources["item91"];
                isValid = false;
            }

            if (!decimal.TryParse(CostFactor, out decimal _))
            {
                CostFactorError = (string)System.Windows.Application.Current.Resources["item92"];
                isValid = false;
            }
            if (SelectedLocation == null)
            {
                LocationError = (string)System.Windows.Application.Current.Resources["item61"];
            }
            return isValid;
        }
        #endregion

    }

}
