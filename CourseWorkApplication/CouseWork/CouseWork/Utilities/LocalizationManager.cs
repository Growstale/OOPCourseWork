using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace CouseWork.Utilities
{
    public class LocalizationManager : INotifyPropertyChanged // Singleton
    {
        #region Fields

        private static LocalizationManager? _instance;
        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private CultureInfo _currentCulture;
        public CultureInfo CurrentCulture // текущая культура
        {
            get => _currentCulture;
            set
            {
                if (_currentCulture != value)
                {
                    _currentCulture = value;
                    OnPropertyChanged(nameof(CurrentCulture));
                    UpdateResources();
                }
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion 

        #region Methods

        private void UpdateResources()
        {
            var dictionary = new ResourceDictionary
            {
                Source = new Uri($"Dictionaries/Resources.{_currentCulture.Name}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary); // добавление нового словаря в
                                                                                // коллекцию объединенных словарей ресурсов приложения
        }

        private LocalizationManager()
        {
            _currentCulture = new CultureInfo("ru"); 
            UpdateResources();

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(CurrentCulture))
                {
                    UpdateResources();
                }
            };
        }

        #endregion
    }
}
