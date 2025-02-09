using CouseWork.Data;
using CouseWork.Utilities;
using CouseWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CouseWork.Views
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        public void OpenMainClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainViewModel = new();
            mainViewModel.Show();
            this.Close();
        }
        private void SwitchLanguage_Click(object sender, RoutedEventArgs e)
        {
            var currentCulture = LocalizationManager.Instance.CurrentCulture.Name;
            if (currentCulture == "ru")
            {
                LocalizationManager.Instance.CurrentCulture = new CultureInfo("en");
            }
            else
            {
                LocalizationManager.Instance.CurrentCulture = new CultureInfo("ru");
            }
        }

    }
}
