using CouseWork.Context;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CouseWork.Data;
using CouseWork.Views;
using System.Globalization;
using CouseWork.Utilities;
using CouseWork.ViewModels;
using CouseWork.Utilities;
namespace CouseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UnitOfWork unitOfWork;
        public MainWindow()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            if (AuthorizationViewModel.SubscriberCount == 0)
            {
                AuthorizationViewModel.RequestClose += AuthorizationViewModel_RequestClose;
            }
        }

        private void AuthorizationViewModel_RequestClose()
        {
            AuthorizationViewModel.RequestClose -= AuthorizationViewModel_RequestClose;
            MainCatalog catalog = new();
            catalog.Show();
            this.Close();
        }

        public void OpenRegistrationClick(object sender, RoutedEventArgs e)
        {
            Registration authorization = new();
            authorization.Show();
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