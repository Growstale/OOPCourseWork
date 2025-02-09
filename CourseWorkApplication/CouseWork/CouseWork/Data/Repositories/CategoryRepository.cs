using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CouseWork.Commands;
using CouseWork.Data;
using System.Configuration;
using CouseWork.Views;
using CouseWork.Utilities;
using CouseWork.Models;
using System.Collections.ObjectModel;
using CouseWork.Context;

namespace CouseWork.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Users>
    {
        ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ObservableCollection<Categories> GetCategories()
        {
            var categories = _context.Categories.ToList();

            return new ObservableCollection<Categories>(categories);
        }
        public Categories? FindById(int id)
        {
            return _context.Categories.FirstOrDefault(l => l.CategoryID == id);
        }
        public Categories? UpdateCategory(int id, string name)
        {
            var category = FindById(id);

            if (!CheckCategoryUniqueExceptCurrent(name, id))
            {
                if (category != null)
                {

                    category.CategoryName = name;
                    _context.SaveChanges();
                    return category;
                }
                return null;
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["item192"]);
            }
            return null;
        }
        public Categories? AddCategory(string name)
        {
            if (!CheckCategoryUnique(name))
            {
                var maxCategoryId = _context.Categories
                                .Max(l => (int?)l.CategoryID) + 1 ?? 1;
                var category = new Categories { CategoryID = maxCategoryId, CategoryName = name };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            return null;
        }

        public Categories? DeleteCategory(int id)
        {
            if (_context.Events.Count(e => e.CategoryID == id) == 0)
            {
                var category = _context.Categories.FirstOrDefault(l => l.CategoryID == id);

                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return category;
                }
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["item41"]);
            }
            return null;
        }
        public bool CheckCategoryUnique(string name)
        {
            return _context.Categories.Any(l => l.CategoryName == name);
        }
        public bool CheckCategoryUniqueExceptCurrent(string name, int id)
        {
            return _context.Categories.Any(l => l.CategoryName == name && l.CategoryID != id);
        }

    }
}
