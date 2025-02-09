using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using CouseWork.Data;
using CouseWork.Utilities;

namespace CouseWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LocalizationManager.Instance.CurrentCulture = new CultureInfo("ru");
        }

    }

}
