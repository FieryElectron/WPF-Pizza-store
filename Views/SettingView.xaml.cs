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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using PizzaStore.Classes;
using System.Windows.Threading;

namespace PizzaStore.Views
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl {

        public SettingView() {
            InitializeComponent();
        }

        private void OnLoadEvent(object sender, RoutedEventArgs e) {
            this.DataContext = App.AppLocalStorage.AppSetting;
            PayStatusComboBox.ItemsSource = new List<string> { "Paied", "UnPay" };
        }

        private void TimerUpdatePizzaPriceLostFocus(object sender, EventArgs e) {
            (sender as DispatcherTimer).Stop();
            Helper.UpdatePizzaPrice(App.AppLocalStorage.AppSetting.TemplatePizza);
        }

        private void UpdatePizzaPriceLostFocus(object sender, RoutedEventArgs e) {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1);
            dt.Tick += TimerUpdatePizzaPriceLostFocus;
            dt.Start();
        }


    }
}
