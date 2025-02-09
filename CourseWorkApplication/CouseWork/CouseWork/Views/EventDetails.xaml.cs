using CouseWork.ViewModels;
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
    /// Логика взаимодействия для EventDetails.xaml
    /// </summary>
    public partial class EventDetails : Window
    {
        public EventDetails()
        {
            InitializeComponent();
            var viewModel = new EventDetailsViewModel();
            DataContext = viewModel;
        }
        public void ExitClick(object sender, RoutedEventArgs e)
        {
            MainCatalog mainCatalog = new();
            mainCatalog.Show();
            this.Close();
        }

    }
    public class TicketTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TicketTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TicketViewModel)
            {
                return TicketTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
