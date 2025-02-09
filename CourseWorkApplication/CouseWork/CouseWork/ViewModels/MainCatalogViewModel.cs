using CouseWork.Commands;
using CouseWork.Data;
using CouseWork.Data.Repositories;
using CouseWork.Models;
using CouseWork.Views;
using CouseWork.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CouseWork.ViewModels
{
    public class MainCatalogViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event Action RequestClose; // Событие, запрашивающее закрытие окна

        UnitOfWork unitOfWork;
        private ObservableCollection<EventsSchedule> _movies;
        public ObservableCollection<EventsSchedule> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }
        private ObservableCollection<Categories> _categories;
        public ObservableCollection<Categories> Categories {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        private ObservableCollection<Locations> _locations;
        public ObservableCollection<Locations> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }
        private ObservableCollection<EventsSchedule> _filteredMovies;

        public ObservableCollection<EventsSchedule> FilteredMovies
        {
            get { return _filteredMovies; }
            set
            {
                _filteredMovies = value;
                OnPropertyChanged(nameof(FilteredMovies));
            }
        }

        private Categories _selectedCategory;
        public Categories SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        private int _selectedSession;

        public int SelectedSession
        {
            get { return _selectedSession; }
            set
            {
                if (_selectedSession != value)
                {
                    _selectedSession = value;
                    OnPropertyChanged(nameof(SelectedSession));
                }
            }
        }
        private Locations _selectedLocation;

        public Locations SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                if (_selectedLocation != value)
                {
                    _selectedLocation = value;
                    OnPropertyChanged(nameof(SelectedLocation));
                }
            }
        }

        private GRN _selectedGRN;
        public GRN selectedGRN
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

        public DateTime MinDate { get; set; } = DateTime.Now;
        public DateTime MaxDate { get; set; } = DateTime.Now.AddYears(1);

        private string _selectedText;
        public string SelectedText
        {
            get => _selectedText;
            set
            {
                if (_selectedText != value)
                {
                    _selectedText = value;
                    OnPropertyChanged(nameof(SelectedText));
                }
            }
        }

        private string _text1 = "❌"; 
        private string _text2 = "📅";
        private string _text3 = "↑";

        public string Text1
        {
            get => _text1;
            set
            {
                if (_text1 != value)
                {
                    _text1 = value;
                    OnPropertyChanged(nameof(Text1));
                }
            }
        }

        public string Text2
        {
            get => _text2;
            set
            {
                if (_text2 != value)
                {
                    _text2 = value;
                    OnPropertyChanged(nameof(Text2));
                }
            }
        }

        public string Text3
        {
            get => _text3;
            set
            {
                if (_text3 != value)
                {
                    _text3 = value;
                    OnPropertyChanged(nameof(Text3));
                }
            }
        }
        public ICommand ApplyFiltersCommand { get; }
        public ICommand OpenMovieDetailsCommand { get; }
        public ICommand OpenAfishaCommand { get; }
        public ICommand OpenProfileCommand { get; }
        public ICommand OpenSupportCommand { get; }
        public ICommand ToggleTextCommand { get; }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        public MainCatalogViewModel()
        {
            unitOfWork = new UnitOfWork();
            Movies = unitOfWork.EventScheduleRepository.GetEventSchedules();


            Categories = new ObservableCollection<Categories>
            {
                new Categories { CategoryID = 0, CategoryName = "-" }
            };
            var otherCategories = unitOfWork.CategoryRepository.GetCategories();
            foreach (var category in otherCategories)
            {
                Categories.Add(category);
            }
            SelectedCategory = Categories.First();


            Locations = new ObservableCollection<Locations>
            {
                new Locations { LocationID = 0, LocationName = "-",NumberOfSectors = 1 }
            };
            var otherLocations = unitOfWork.LocationRepository.GetLocations();
            foreach (var location in otherLocations)
            {
                Locations.Add(location);
            }
            SelectedLocation = Locations.First();


            FilteredMovies = new ObservableCollection<EventsSchedule>(
                unitOfWork.EventScheduleRepository.GetEventSchedules()
                .Where(es => es.EventDate > DateTime.Now)
            );

            OpenMovieDetailsCommand = new RelayCommand(OpenMovieDetails, CanExecuteLogin);
            ToggleTextCommand = new RelayCommand(ChangeText);

            MinDate = DateTime.Now;
            MaxDate = DateTime.Now.AddYears(1);

            selectedGRN = new GRN
            {
                grnDate = DateTime.Today
            };

            selectedGRN.PropertyChanged += SelectedGRN_PropertyChanged; // Подписка на событие
            PropertyChanged += MainCatalogViewModel_PropertyChanged;

            ApplyFilters();
        }

        private bool CanExecuteLogin(object parameter)
        {
            return true;
        }

        private void MainCatalogViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
                ApplyFilters();
        }

        private void SelectedGRN_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GRN.grnDate))
            {
                ApplyFilters(); 
            }
        }

        private void ApplyFilters()
        {
            var filtered = Movies.Where(m =>
                (SelectedLocation.LocationName == "-" || m.LocationID == SelectedLocation.LocationID) &
                (SelectedCategory.CategoryName == "-" || m.Event.CategoryID == SelectedCategory.CategoryID) &
                (SelectedSession == 0 ||
                 (SelectedSession == 1 && m.EventDate.TimeOfDay >= new TimeSpan(6, 0, 0) && m.EventDate.TimeOfDay < new TimeSpan(12, 0, 0)) ||
                 (SelectedSession == 2 && m.EventDate.TimeOfDay >= new TimeSpan(12, 0, 0) && m.EventDate.TimeOfDay < new TimeSpan(18, 0, 0)) ||
                 (SelectedSession == 3 && m.EventDate.TimeOfDay >= new TimeSpan(18, 0, 0) && m.EventDate.TimeOfDay < new TimeSpan(24, 0, 0)) ||
                 (SelectedSession == 4 && m.EventDate.TimeOfDay >= new TimeSpan(0, 0, 0) && m.EventDate.TimeOfDay < new TimeSpan(6, 0, 0))) &
                (Text1 == "❌" || (m.EventDate.Date == selectedGRN.grnDate && Text1 == "✅")) &
                m.EventDate > DateTime.Now
                );

            if (Text2 == "📅" && Text3 == "↑")
                filtered = filtered.OrderBy(m => m.EventDate);
            else if (Text2 == "📅" && Text3 == "↓")
                filtered = filtered.OrderByDescending(m => m.EventDate);
            else if (Text2 == "💲" && Text3 == "↑")
                filtered = filtered.OrderBy(m => m.Event.Cost);
            else if (Text2 == "💲" && Text3 == "↓")
                filtered = filtered.OrderByDescending(m => m.Event.Cost);

            FilteredMovies.Clear();
            foreach (var movie in filtered)
                FilteredMovies.Add(movie);
        }
        private void OpenMovieDetails(object parameter)
        {
            if (parameter is EventsSchedule schedule)
            {
                var eventScheduleId = schedule.EventScheduleID;
                var detailsWindow = new EventDetails
                {
                    DataContext = new EventDetailsViewModel(eventScheduleId)
                };
                detailsWindow.Show();
                RequestClose?.Invoke();
            }
        }

        private void ChangeText(object parameter)
        {
            if (parameter is string propertyName)
            {
                switch (propertyName)
                {
                    case nameof(Text1):
                        Text1 = Text1 == "❌" ? "✅" : "❌";
                        break;
                    case nameof(Text2):
                        Text2 = Text2 == "📅" ? "💲" : "📅";
                        break;
                    case nameof(Text3):
                        Text3 = Text3 == "↑" ? "↓" : "↑";
                        break;
                }
            }
        }
        #endregion
    }
}
