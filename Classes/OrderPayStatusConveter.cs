using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.TextFormatting;

namespace PizzaStore.Classes
{
    public class OrderPayStatusConveter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            bool paystatus = (bool)value;
            string text;
            if (paystatus) {
                text = "Paied";
            } else {
                text = "UnPay";
            }

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

            string paystatus = (string)value;
            bool boolean;
            if (paystatus == "Paied") {
                boolean = true;
            } else {
                boolean = false;
            }

            return boolean;
        }

    }

}
