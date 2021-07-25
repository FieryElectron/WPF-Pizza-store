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
using System.Windows.Media.Animation;

namespace PizzaStore.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {

        public OrderView() {
            InitializeComponent();
        }

        private void OnLoadEvent(object sender, RoutedEventArgs e) {
            PayStatusComboBox.ItemsSource = new List<string> { "Paied", "UnPay" };
            DataContext = PizzaStore.App.AppLocalStorage.AppOrderList;
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e) {
            if (OrderHistoryListBox.SelectedItem == null) {
                MessageBox.Show("Please select Order first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var Order = OrderHistoryListBox.SelectedItem as Order;

            if (Order != null) {
                var result = MessageBox.Show($"Sure about Cancel Order {Order.ID} ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK) {
                    App.AppLocalStorage.AppOrderList.Remove(Order);
            
                    OrderHistorySearchOnChanged(OrderSearchText, null);
                }

            }

        }

        private void CancelReOrder_Click(object sender, RoutedEventArgs e) {
            if (OrderHistoryListBox.SelectedItem == null) {
                MessageBox.Show("Please select Order first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var Order = OrderHistoryListBox.SelectedItem as Order;

            if (Order != null) {
                var result = MessageBox.Show($"Sure about Cancel & Order {Order.ID} ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK) {
                    App.AppLocalStorage.AppTheOrder.PizzaList.Clear();

                    App.AppLocalStorage.AppTheOrder = Order;

                    App.AppLocalStorage.AppOrderList.Remove(Order);
                    OrderHistorySearchOnChanged(OrderSearchText, null);

                    var win = Window.GetWindow(this) as MainWindow;
                    if (win != null) {
                        win.SwitchView_Clicked(win.TakeOrderButton, null);
                    }
                }

            }
        }

        private void OrderHistorySearchOnChanged(object sender, TextChangedEventArgs e) {
            var input = (sender as TextBox).Text.ToLower();
            var FilterResult = from obj in App.AppLocalStorage.AppOrderList
                               where obj.ID.ToLower().Contains(input) || 
                               obj.CustomerFirstName.ToLower().Contains(input) ||
                               obj.CustomerLastName.ToLower().Contains(input) ||
                               obj.CustomerPhone.ToLower().Contains(input) ||
                               obj.CustomerEmail.ToLower().Contains(input) ||
                               obj.CustomerAddress.ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input) ||
                               obj.Date.ToLower().Contains(input) ||
                               obj.Time.ToLower().Contains(input) ||
                               Helper.ConvetBoolToStringPayStatue(obj.PayStatus).ToLower().Contains(input)
                               select obj;

            OrderHistoryListBox.ItemsSource = FilterResult;
        }

        private void OrderPizzaSearchOnChanged(object sender, TextChangedEventArgs e) {
            var selected = OrderHistoryListBox.SelectedItem;
            if (selected == null) {
                return;
            }

            var input = (sender as TextBox).Text.ToLower();
            var order = selected as Order;

            var FilterResult = from obj in order.PizzaList
                               where obj.Name.ToLower().Contains(input) ||
                               obj.SizePriceUnit.ToString().ToLower().Contains(input) ||
                               obj.Size.ToString().ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input) ||
                               obj.Quantity.ToString().ToLower().Contains(input) ||
                               obj.TotalPrice.ToString().ToLower().Contains(input)
                               select obj;

            OrderPizzaListBox.ItemsSource = FilterResult;
        }

        private void OrderSelectionOnChanged(object sender, SelectionChangedEventArgs e) {
            OrderPizzaSearchOnChanged(PizzaListSearchText, null);
        }
    }
}
