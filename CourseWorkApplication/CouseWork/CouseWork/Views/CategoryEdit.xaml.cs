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

namespace CouseWork.ViewModels
{
    /// <summary>
    /// Логика взаимодействия для CategoryEdit.xaml
    /// </summary>
    public partial class CategoryEdit : UserControl
    {
        public CategoryEdit()
        {
            InitializeComponent();
        }
    }
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                "On sale" => Brushes.Green,
                "Booked" => Brushes.Yellow,
                "Saled" => Brushes.Red,
                _ => Brushes.Gray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
