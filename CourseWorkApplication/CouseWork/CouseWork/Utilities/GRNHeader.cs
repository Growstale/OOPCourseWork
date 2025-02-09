using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Utilities
{
    public class GRNHeader : INotifyPropertyChanged // инкапсуляция даты + механизм уведомления об её изменениях
    {
        private DateTime? _grnDate;
        public DateTime? grnDate
        {
            get => _grnDate;
            set
            {
                if (_grnDate != value)
                {
                    _grnDate = value;
                    OnPropertyChanged(nameof(grnDate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class GRN : INotifyPropertyChanged
    {
        private DateTime? _grnDate;
        public DateTime? grnDate
        {
            get => _grnDate;
            set
            {
                if (_grnDate != value)
                {
                    _grnDate = value;
                    OnPropertyChanged(nameof(grnDate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
