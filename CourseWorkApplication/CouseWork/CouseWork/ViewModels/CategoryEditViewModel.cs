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
using System.Windows;
using System.Windows.Input;

namespace CouseWork.ViewModels
{
    public class CategoryEditViewModel : INotifyPropertyChanged
    {
        #region Fields
        private UnitOfWork unitOfWork;
        public ICommand SaveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        private ObservableCollection<Categories> _categories;
        public ObservableCollection<Categories> Categories
        {
            get => _categories;
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }

        private Categories _selectedCategory;
        public Categories SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));

                if (value != null)
                {
                    Id = value.CategoryID.ToString();
                    Name = value.CategoryName;
                }
                else
                {
                    Id = string.Empty;
                    Name = string.Empty;
                }
            }
        }

        private string _id;
        private string _name;
        private string _idError;
        private string _nameError;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
                IdError = string.Empty;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                NameError = string.Empty;
            }
        }

        public string IdError
        {
            get => _idError;
            set
            {
                _idError = value;
                OnPropertyChanged(nameof(IdError));
            }
        }

        public string NameError
        {
            get => _nameError;
            set
            {
                _nameError = value;
                OnPropertyChanged(nameof(NameError));
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods
        public CategoryEditViewModel()
        {
            unitOfWork = new UnitOfWork();
            Categories = unitOfWork.CategoryRepository.GetCategories();
            SaveCommand = new RelayCommand(SaveChanges);
            AddCommand = new RelayCommand(AddCategory);
            DeleteCommand = new RelayCommand(DeleteCategory);
        }

        private void CategoriesCollectionChanged()
        {
            var updatedCategories = unitOfWork.CategoryRepository.GetCategories();
            Categories.Clear();
            foreach (var category in updatedCategories)
            {
                Categories.Add(category);
            }
        }

        private void ResetErrorFields()
        {
            IdError = string.Empty;
            NameError = string.Empty;
        }

        private void SaveChanges(object parameter)
        {
            if (ValidateFieldsUpdate())
            {
                if (SelectedCategory != null)
                {
                    var updatedCategory = unitOfWork.CategoryRepository.UpdateCategory(SelectedCategory.CategoryID, Name);
                    if (updatedCategory != null)
                    {
                        var index = Categories.IndexOf(SelectedCategory);
                        Categories.RemoveAt(index);
                        Categories.Insert(index, updatedCategory);
                    }
                }
            }
        }

        private void AddCategory(object parameter)
        {
            if (ValidateFieldsAdd())
            {
                var newCategory = unitOfWork.CategoryRepository.AddCategory(Name);
                if (newCategory != null) Categories.Add(newCategory);
            }
        }

        private void DeleteCategory(object parameter)
        {
            if (ValidateFieldsDelete())
            {
                if (SelectedCategory != null)
                {
                    var deletedCategory = unitOfWork.CategoryRepository.DeleteCategory(SelectedCategory.CategoryID);
                    if (deletedCategory != null) Categories.Remove(deletedCategory);
                }
            }
        }

        private bool ValidateFieldsUpdate()
        {
            ResetErrorFields();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Id))
            {
                IdError = (string)Application.Current.Resources["item38"];
                isValid = false;
                return isValid;
            }

            if (!string.IsNullOrWhiteSpace(Name) && Name.Length > 40)
            {
                NameError = (string)Application.Current.Resources["item70"];
                isValid = false;
            }


            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = (string)Application.Current.Resources["item39"];
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateFieldsAdd()
        {
            ResetErrorFields();
            bool isValid = true;

            if (unitOfWork.CategoryRepository.CheckCategoryUnique(Name))
            {
                NameError = (string)Application.Current.Resources["item40"];
                isValid = false;
            }

            if (!string.IsNullOrWhiteSpace(Name) && Name.Length > 40)
            {
                NameError = (string)Application.Current.Resources["item70"];
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = (string)Application.Current.Resources["item39"];
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
                IdError = (string)Application.Current.Resources["item38"];
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
