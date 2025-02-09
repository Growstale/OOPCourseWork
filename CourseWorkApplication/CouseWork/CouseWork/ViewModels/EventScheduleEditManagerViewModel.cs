using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using CouseWork.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
namespace CouseWork.ViewModels
{
    class EventScheduleEditManagerViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly UnitOfWork _unitOfWork;

        public DateTime MinDate { get; set; } = DateTime.Now;
        public DateTime MaxDate { get; set; } = DateTime.Now.AddYears(1);

        private GRNHeader _selectedGRN;
        public GRNHeader selectedGRN
        {
            get => _selectedGRN;
            set
            {
                if (_selectedGRN != value)
                {
                    _selectedGRN = value;
                    OnPropertyChanged(nameof(selectedGRN));
                }
            }
        }

        private ObservableCollection<EventsSchedule> _eventSchedules;

        public ObservableCollection<EventsSchedule> EventSchedules
        {
            get => _eventSchedules;
            set
            {
                _eventSchedules = value;
                OnPropertyChanged(nameof(EventSchedules));
            }
        }
        private ObservableCollection<Locations> _locations;

        public ObservableCollection<Locations> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }
        private ObservableCollection<Events> _events;
        public ObservableCollection<Events> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private EventsSchedule _selectedEventSchedule;
        public EventsSchedule SelectedEventSchedule
        {
            get => _selectedEventSchedule;
            set
            {
                _selectedEventSchedule = value;
                OnPropertyChanged(nameof(SelectedEventSchedule));

                if (value != null)
                {
                    EventScheduleID = value.EventScheduleID.ToString();
                    selectedGRN.grnDate = value.EventDate;
                    SelectedEvent = value.Event;
                    SelectedLocation = value.Location;
                    EventTime = value.EventDate.ToString("HH:mm");
                }
            }
        }

        private DateTime _eventDate;
        public DateTime EventDate
        {
            get => _eventDate;
            set
            {
                _eventDate = value;
                OnPropertyChanged(nameof(EventDate));
                EventDateError = string.Empty;
            }
        }
        private string _eventDateError;
        public string EventDateError
        {
            get => _eventDateError;
            set
            {
                _eventDateError = value;
                OnPropertyChanged(nameof(EventDateError));
            }
        }

        private string _eventScheduleID;
        public string EventScheduleID
        {
            get => _eventScheduleID;
            set
            {
                _eventScheduleID = value;
                OnPropertyChanged(nameof(EventScheduleID));
                EventScheduleIDError = string.Empty;
            }
        }
        private string _eventScheduleIDError;
        public string EventScheduleIDError
        {
            get => _eventScheduleIDError;
            set
            {
                _eventScheduleIDError = value;
                OnPropertyChanged(nameof(EventScheduleIDError));
            }
        }

        public Locations _selectedLocation;
        public Locations SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
                SelectedLocationError = string.Empty;
            }
        }

        public string _selectedLocationError;
        public string SelectedLocationError
        {
            get => _selectedLocationError;
            set
            {
                _selectedLocationError = value;
                OnPropertyChanged(nameof(SelectedLocationError));
            }
        }

        private Events _selectedEvent;
        public Events SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
                SelectedEventError = string.Empty;
            }
        }

        private string _selectedEventError;
        public string SelectedEventError
        {
            get => _selectedEventError;
            set
            {
                _selectedEventError = value;
                OnPropertyChanged(nameof(SelectedEventError));
            }
        }

        private string _eventTime;
        public string EventTime
        {
            get => _eventTime;
            set
            {
                _eventTime = value;
                OnPropertyChanged(nameof(EventTime));
                EventTimeError = string.Empty;
            }
        }

        private string _eventTimeError;
        public string EventTimeError
        {
            get => _eventTimeError;
            set
            {
                _eventTimeError = value;
                OnPropertyChanged(nameof(EventTimeError));
            }
        }
        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods
        public EventScheduleEditManagerViewModel()
        {
            _unitOfWork = new UnitOfWork();

            EventSchedules = _unitOfWork.EventScheduleRepository.GetEventSchedules();
            Locations = _unitOfWork.LocationRepository.GetLocations();
            Events = _unitOfWork.EventRepository.GetEvents();

            AddCommand = new RelayCommand(AddEventSchedule);
            SaveCommand = new RelayCommand(SaveChanges);
            DeleteCommand = new RelayCommand(DeleteEventSchedule);

            selectedGRN = new GRNHeader
            {
                grnDate = DateTime.Today
            };
        }

        private void ResetErrorFields()
        {
            EventDateError = string.Empty;
            EventScheduleIDError = string.Empty;
            SelectedLocationError = string.Empty;
            SelectedEventError = string.Empty;
            EventTimeError = string.Empty;
        }


        private void AddEventSchedule(object parameter)
        {
            if (ValidateFieldsAdd())
            {
                if (TimeSpan.TryParseExact(EventTime, @"hh\:mm", null, out TimeSpan time))
                {
                    DateTime calendartime = (DateTime)selectedGRN.grnDate;
                    DateTime outputtime = calendartime.Date + time;


                    var newSchedule = _unitOfWork.EventScheduleRepository.AddEventSchedule(outputtime, SelectedEvent.EventID, SelectedLocation.LocationID);
                    EventSchedules = _unitOfWork.EventScheduleRepository.GetEventSchedules();
                }
            }
        }

        private void SaveChanges(object parameter)
        {
            if (ValidateFieldsUpdate())
            {
                if (TimeSpan.TryParseExact(EventTime, @"hh\:mm", null, out TimeSpan time))
                {
                    DateTime calendartime = (DateTime)selectedGRN.grnDate;
                    DateTime outputtime = calendartime.Date + time;

                    _unitOfWork.EventScheduleRepository.UpdateEventSchedule(SelectedEventSchedule.EventScheduleID, outputtime, SelectedEvent.EventID, SelectedLocation.LocationID);
                    EventSchedules = _unitOfWork.EventScheduleRepository.GetEventSchedules();
                }
            }
        }

        private void DeleteEventSchedule(object parameter)
        {
            if (ValidateFieldsDelete())
            {
                _unitOfWork.EventScheduleRepository.DeleteEventSchedule(SelectedEventSchedule.EventScheduleID);
                EventSchedules = _unitOfWork.EventScheduleRepository.GetEventSchedules();
            }
        }

        private bool ValidateFieldsUpdate()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(EventScheduleID))
            {
                EventScheduleIDError = (string)Application.Current.Resources["item60"];
                isValid = false;
                return isValid;
            }
            bool validTime = true;
            DateTime outputtime = DateTime.Now;
            if (SelectedLocation == null)
            {
                SelectedLocationError = (string)Application.Current.Resources["item61"];
                isValid = false;

            }

            if (SelectedEvent == null)
            {
                SelectedEventError = (string)Application.Current.Resources["item51"];
                isValid = false;
            }

            if (SelectedEvent != null && (DateTime)selectedGRN.grnDate < SelectedEvent.StartDate)
            {
                EventDateError = $"{(string)Application.Current.Resources["item63"]} ({SelectedEvent.StartDate})";
                isValid = false;
            }

            string pattern = @"^([0-1]?[0-9]|2[0-3]):([0-5]?[0-9])$";
            if (!string.IsNullOrWhiteSpace(EventTime) && !Regex.IsMatch(EventTime, pattern))
            {
                EventTimeError = (string)Application.Current.Resources["item64"];
                isValid = false;
                validTime = false;
            }

            if (string.IsNullOrWhiteSpace(EventTime))
            {
                EventTimeError = (string)Application.Current.Resources["item65"];
                isValid = false;
                validTime = false;
            }

            if (validTime)
            {
                if (!string.IsNullOrWhiteSpace(EventTime) && TimeSpan.TryParseExact(EventTime, @"hh\:mm", null, out TimeSpan time))
                {
                    DateTime calendartime = (DateTime)selectedGRN.grnDate;
                    outputtime = calendartime.Date + time;

                    if (outputtime < DateTime.Now)
                    {
                        EventDateError = (string)Application.Current.Resources["item169"];
                        isValid = false;
                    }

                    if ((SelectedLocation != null && SelectedEvent != null) &&
                         _unitOfWork.EventScheduleRepository.CheckEventScheduleTimeExceptItself(outputtime, SelectedEvent.EventID, SelectedLocation.LocationID, SelectedEventSchedule.EventScheduleID))
                    {
                        EventDateError = (string)Application.Current.Resources["item66"];
                        isValid = false;
                    }

                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool ValidateFieldsAdd()
        {
            ResetErrorFields();
            bool isValid = true;
            bool validTime = true;
            DateTime outputtime = DateTime.Now;
            if (SelectedLocation == null)
            {
                SelectedLocationError = (string)Application.Current.Resources["item61"];
                isValid = false;

            }

            if (SelectedEvent == null)
            {
                SelectedEventError = (string)Application.Current.Resources["item51"];
                isValid = false;
            }

            if (SelectedEvent != null && (DateTime)selectedGRN.grnDate < SelectedEvent.StartDate)
            {
                EventDateError = $"{(string)Application.Current.Resources["item63"]} ({SelectedEvent.StartDate})";
                isValid = false;
            }

            string pattern = @"^([0-1]?[0-9]|2[0-3]):([0-5]?[0-9])$";
            if (!string.IsNullOrWhiteSpace(EventTime) && !Regex.IsMatch(EventTime, pattern))
            {
                EventTimeError = (string)Application.Current.Resources["item64"];
                isValid = false;
                validTime = false;
            }

            if (string.IsNullOrWhiteSpace(EventTime))
            {
                EventTimeError = (string)Application.Current.Resources["item65"];
                isValid = false;
                validTime = false;
            }

            if (validTime)
            {
                if (!string.IsNullOrWhiteSpace(EventTime) && TimeSpan.TryParseExact(EventTime, @"hh\:mm", null, out TimeSpan time))
                {
                    DateTime calendartime = (DateTime)selectedGRN.grnDate;
                    outputtime = calendartime.Date + time;

                    if (outputtime < DateTime.Now)
                    {
                        EventDateError = (string)Application.Current.Resources["item169"];
                        isValid = false;
                    }

                    if ((SelectedLocation != null && SelectedEvent != null) &&
                         _unitOfWork.EventScheduleRepository.CheckEventScheduleTime(outputtime, SelectedEvent.EventID, SelectedLocation.LocationID))
                    {
                        EventDateError = (string)Application.Current.Resources["item66"];
                        isValid = false;
                    }

                }
                else
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool ValidateFieldsDelete()
        {
            ResetErrorFields();
            bool isValid = true;


            if (string.IsNullOrWhiteSpace(EventScheduleID))
            {
                EventScheduleIDError = (string)Application.Current.Resources["item60"];
                isValid = false;
            }

            return isValid;
        }
        #endregion
    }
}
