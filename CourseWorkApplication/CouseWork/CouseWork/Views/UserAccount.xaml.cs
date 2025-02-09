using CouseWork.Views;
using System;
using System.Collections.Generic;
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

namespace CouseWork.ViewModels
{
    /// <summary>
    /// Логика взаимодействия для UserAccount.xaml
    /// </summary>
    public partial class UserAccount : Window
    {
        public UserAccount()
        {
            InitializeComponent();
        }
        public void ExitClick(object sender, RoutedEventArgs e)
        {
            MainCatalog mainCatalog = new();
            mainCatalog.Show();
            this.Close();
        }

    }
}
