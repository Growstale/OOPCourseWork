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

namespace CouseWork.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerAccount.xaml
    /// </summary>
    public partial class ManagerAccount : Window
    {
        public ManagerAccount()
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
