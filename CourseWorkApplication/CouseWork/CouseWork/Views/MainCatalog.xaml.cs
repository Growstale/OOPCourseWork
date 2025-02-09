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
using CouseWork.Data;
using CouseWork.Utilities;
using CouseWork.ViewModels;

namespace CouseWork.Views
{
    public partial class MainCatalog : Window
    {
        UnitOfWork unitOfWork;
        public MainCatalog()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            var viewModel = new MainCatalogViewModel();
            DataContext = viewModel;
            viewModel.RequestClose += () => this.Close(); // ждём события на закрытие окна

        }
        public void OpenAccount(object sender, RoutedEventArgs e)
        {
            switch (unitOfWork.EventRepository.FindRoleId(UserSession.CurrentUserID))
            {
                case 1:
                    {
                        UserAccount userAccount = new UserAccount();
                        userAccount.Show();
                        this.Close();
                        break;
                    }
                case 3:
                    {
                        OrganizerAccount organizerAccount = new();
                        organizerAccount.Show();
                        this.Close();
                        break;
                    }
                case 2:
                    {
                        ManagerAccount managerAccount = new();
                        managerAccount.Show();
                        this.Close();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Error");
                        break;
                    }
            }
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
        public void CloseAccount(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            this.Close();
        }
    }
}
