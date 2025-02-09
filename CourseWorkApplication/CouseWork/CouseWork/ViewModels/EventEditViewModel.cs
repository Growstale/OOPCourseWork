using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Models;
using CouseWork.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CouseWork.ViewModels
{
    public class EventEditViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Categories> _categories;
        public ObservableCollection<Categories> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
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

                if (value != null)
                {
                    EventID = value.EventID.ToString();
                    EventName = value.EventName;
                    EventDuration = value.EventDuration.ToString();
                    SelectedCategory = value.Category;
                    Cost = value.Cost.ToString();
                    Description = value.Description;
                    EventImage = ByteArrayToImage(value.Image);
                    selectedGRN.grnDate = value.StartDate;
                }
                else
                {
                    ResetFields();
                }
            }
        }
        private string _eventID;
        private string _eventIDError;
        public string EventID
        {
            get { return _eventID; }
            set
            {
                _eventID = value;
                OnPropertyChanged(nameof(EventID));
                EventIDError = string.Empty;
            }
        }
        public string EventIDError
        {
            get { return _eventIDError; }
            set
            {
                _eventIDError = value;
                OnPropertyChanged(nameof(EventIDError));
            }
        }

        private string _eventName;
        private string _eventNameError;
        public string EventName
        {
            get { return _eventName; }
            set
            {
                _eventName = value;
                OnPropertyChanged(nameof(EventName));
                EventNameError = string.Empty;
            }
        }
        public string EventNameError
        {
            get { return _eventNameError; }
            set
            {
                _eventNameError = value;
                OnPropertyChanged(nameof(EventNameError));
            }
        }

        private string _eventDuration;
        private string _eventDurationError;
        public string EventDuration
        {
            get { return _eventDuration; }
            set
            {
                _eventDuration = value;
                OnPropertyChanged(nameof(EventDuration));
                EventDurationError = string.Empty;
            }
        }
        public string EventDurationError
        {
            get { return _eventDurationError; }
            set
            {
                _eventDurationError = value;
                OnPropertyChanged(nameof(EventDurationError));
            }
        }

        private Categories _selectedCategory;
        private string _selectedCategoryError;
        public Categories SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                SelectedCategoryError = string.Empty;
            }
        }
        public string SelectedCategoryError
        {
            get { return _selectedCategoryError; }
            set
            {
                _selectedCategoryError = value;
                OnPropertyChanged(nameof(SelectedCategoryError));
            }
        }

        private string _cost;
        private string _costError;
        public string Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
                CostError = string.Empty;
            }
        }
        public string CostError
        {
            get { return _costError; }
            set
            {
                _costError = value;
                OnPropertyChanged(nameof(CostError));
            }
        }

        private string _description;
        private string _descriptionError;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
                DescriptionError = string.Empty;
            }
        }
        public string DescriptionError
        {
            get { return _descriptionError; }
            set
            {
                _descriptionError = value;
                OnPropertyChanged(nameof(DescriptionError));
            }
        }

        private DateTime _startDate;
        private string _startDateError;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                StartDateError = string.Empty;
            }
        }
        public string StartDateError
        {
            get { return _startDateError; }
            set
            {
                _startDateError = value;
                OnPropertyChanged(nameof(StartDateError));
            }
        }

        private ImageSource _eventImage;
        private string _eventImageError;
        public ImageSource EventImage
        {
            get { return _eventImage; }
            set
            {
                _eventImage = value;
                OnPropertyChanged(nameof(EventImage));
                EventImageError = string.Empty;
            }
        }
        public string EventImageError
        {
            get { return _eventImageError; }
            set
            {
                _eventImageError = value;
                OnPropertyChanged(nameof(EventImageError));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UploadImageCommand { get; }


        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods
        public EventEditViewModel()
        {
            _unitOfWork = new UnitOfWork();

            Events = _unitOfWork.EventRepository.GetOrganizerEvents(UserSession.CurrentUserID);
            Categories = _unitOfWork.CategoryRepository.GetCategories();

            SaveCommand = new RelayCommand(SaveChanges);
            AddCommand = new RelayCommand(AddEvent);
            DeleteCommand = new RelayCommand(DeleteEvent);
            UploadImageCommand = new RelayCommand(UploadImage);

            MinDate = DateTime.Now;
            MaxDate = DateTime.Now.AddYears(1);

            selectedGRN = new GRNHeader
            {
                grnDate = DateTime.Today 
            };

        }

        private void ResetFields()
        {
            EventID = string.Empty;
            EventName = string.Empty;
            EventDuration = string.Empty;
            SelectedCategory = null;
            Cost = string.Empty;
            Description = string.Empty;
            EventImage = null;
        }

        private void ResetErrorFields()
        {
            EventIDError = string.Empty;
            EventNameError = string.Empty;
            EventDurationError = string.Empty;
            SelectedCategoryError = string.Empty;
            CostError = string.Empty;
            DescriptionError = string.Empty;
            EventImageError = string.Empty;
        }

        private void SaveChanges(object parameter)
        {
            if (ValidateFieldsUpdate())
            {
                    var updatedEvent = _unitOfWork.EventRepository.UpdateEvent(
                        SelectedEvent.EventID,
                        EventName,
                        TimeSpan.Parse(EventDuration),
                        SelectedCategory.CategoryID,
                        UserSession.CurrentUserID,
                        Description,
                        decimal.Parse(Cost),
                        (DateTime)selectedGRN.grnDate,
                        ImageToByteArray(EventImage)
                    );

                    if (updatedEvent != null)
                    {
                        RefreshEvents();
                    }
                    else
                    {
                    System.Windows.Forms.MessageBox.Show((string)System.Windows.Application.Current.Resources["item48"]);
                    }
                
            }
        }

        private void AddEvent(object parameter)
        {
            if (ValidateFieldsAdd())
            {
                var newEvent = _unitOfWork.EventRepository.AddEvent(
                    EventName,
                    TimeSpan.Parse(EventDuration),
                    SelectedCategory.CategoryID,
                    UserSession.CurrentUserID,
                    Description,
                    decimal.Parse(Cost),
                    (DateTime)selectedGRN.grnDate,
                    ImageToByteArray(EventImage)
                );

                if (newEvent != null)
                {
                    RefreshEvents();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show((string)System.Windows.Application.Current.Resources["item49"]);
                }
            }
        }

        private void DeleteEvent(object parameter)
        {
            if (ValidateFieldsDelete())
            {
                var deletedEvent = _unitOfWork.EventRepository.DeleteEvent(SelectedEvent.EventID);
                if (deletedEvent != null)
                {
                    RefreshEvents();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show((string)System.Windows.Application.Current.Resources["item50"]);
                }
            }
        }

        private void UploadImage(object parameter)
        {
            var dialog = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EventImage = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void RefreshEvents()
        {
            Events = _unitOfWork.EventRepository.GetOrganizerEvents(UserSession.CurrentUserID);
        }

        private byte[] ImageToByteArray(ImageSource imageSource)
        {
            if (imageSource is BitmapSource bitmapSource)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }

        private ImageSource ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null) return null;

            using (var stream = new MemoryStream(byteArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }


        private bool ValidateFieldsUpdate()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(EventID))
            {
                EventIDError = (string)System.Windows.Application.Current.Resources["item51"];
                isValid = false;
                return isValid;
            }

            if (!string.IsNullOrWhiteSpace(EventName) && EventName.Length > 50)
            {
                EventNameError = (string)System.Windows.Application.Current.Resources["item71"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(EventName))
            {
                EventNameError = (string)System.Windows.Application.Current.Resources["item52"];
                isValid = false;
            }

            if (_unitOfWork.EventRepository.CheckEventUniqueExceptCurrent(EventName, int.Parse(EventID)))
            {
                EventNameError = (string)System.Windows.Application.Current.Resources["item59"];
                isValid = false;
            }

            if (TimeSpan.TryParse(EventDuration, out TimeSpan time) && (time < TimeSpan.Zero || time > TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59)))
            {
                EventDurationError = (string)System.Windows.Application.Current.Resources["item53"];
                isValid = false;
            }

            if (!TimeSpan.TryParse(EventDuration, out _))
            {
                EventDurationError = (string)System.Windows.Application.Current.Resources["item54"];
                isValid = false;
            }

            if (SelectedCategory == null)
            {
                SelectedCategoryError = (string)System.Windows.Application.Current.Resources["item55"];
                isValid = false;
            }

            if (decimal.TryParse(Cost, out decimal cost) && (cost < 0 || cost > 9999))
            {
                CostError = (string)System.Windows.Application.Current.Resources["item56"];
                isValid = false;
            }

            if (!decimal.TryParse(Cost, out decimal _))
            {
                CostError = (string)System.Windows.Application.Current.Resources["item78"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Cost))
            {
                CostError = (string)System.Windows.Application.Current.Resources["item77"];
                isValid = false;
            }

            if (selectedGRN.grnDate < DateTime.Today)
            {
                StartDateError = (string)System.Windows.Application.Current.Resources["item57"];

                isValid = false;
            }
            if (EventImage == null)
            {
                EventImageError = (string)System.Windows.Application.Current.Resources["item58"];
                isValid = false;
            }
            if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
            {
                DescriptionError = (string)System.Windows.Application.Current.Resources["item67"];
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateFieldsAdd()
        {
            ResetErrorFields();
            bool isValid = true;

            if (!string.IsNullOrWhiteSpace(EventName) && EventName.Length > 50)
            {
                EventNameError = (string)System.Windows.Application.Current.Resources["item71"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(EventName))
            {
                EventNameError = (string)System.Windows.Application.Current.Resources["item52"];
                isValid = false;
            }

            if (_unitOfWork.EventRepository.CheckEventUnique(EventName))
            {
                EventNameError = (string)System.Windows.Application.Current.Resources["item59"];
                isValid = false;
            }

            if (TimeSpan.TryParse(EventDuration, out TimeSpan time) && (time < TimeSpan.Zero || time > TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59)))
            {
                EventDurationError = (string)System.Windows.Application.Current.Resources["item53"];
                isValid = false;
            }

            if (!TimeSpan.TryParse(EventDuration, out _))
            {
                EventDurationError = (string)System.Windows.Application.Current.Resources["item54"];
                isValid = false;
            }

            if (string.IsNullOrEmpty(EventDuration))
            {
                EventDurationError = (string)System.Windows.Application.Current.Resources["item76"];
                isValid = false;
            }

            if (SelectedCategory == null)
            {
                SelectedCategoryError = (string)System.Windows.Application.Current.Resources["item55"];
                isValid = false;
            }

            if (decimal.TryParse(Cost, out decimal cost) && (cost < 0 || cost > 9999))
            {
                CostError = (string)System.Windows.Application.Current.Resources["item56"];
                isValid = false;
            }

            if (!decimal.TryParse(Cost, out decimal _))
            {
                CostError = (string)System.Windows.Application.Current.Resources["item78"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Cost))
            {
                CostError = (string)System.Windows.Application.Current.Resources["item77"];
                isValid = false;
            }

            if (selectedGRN.grnDate < DateTime.Today)
            {
                StartDateError = (string)System.Windows.Application.Current.Resources["item57"];

                isValid = false;
            }
            if (EventImage == null)
            {
                EventImageError = (string)System.Windows.Application.Current.Resources["item58"];
                isValid = false;
            }
            if (!string.IsNullOrEmpty(Description) && Description.Length > 1000)
            {
                DescriptionError = (string)System.Windows.Application.Current.Resources["item67"];
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateFieldsDelete()
        {
            ResetErrorFields();
            bool isValid = true;


            if (string.IsNullOrWhiteSpace(EventID))
            {
                EventIDError = (string)System.Windows.Application.Current.Resources["item51"];
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
