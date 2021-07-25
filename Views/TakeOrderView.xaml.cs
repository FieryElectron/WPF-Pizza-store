using PizzaStore.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace PizzaStore.Views
{
    /// <summary>
    /// Interaction logic for TakeOrderView.xaml
    /// </summary>
    public partial class TakeOrderView : UserControl
    {
        public TakeOrderView() {
            InitializeComponent();
        }

        private void AddPizzaToOrder_Click(object sender, RoutedEventArgs e) {
            var SelectedItem = Pizza_ListBox.SelectedItem;

            if (SelectedItem == null) {
                MessageBox.Show("Please select Pizza to add to the order!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var piz = SelectedItem as Pizza;

            App.AppLocalStorage.AppTheOrder.PizzaList.Add(new Pizza(piz));
            App.UpdatePizzaTotalPrice();

            TakeOrderPizzaSearchOnChanged(AvailablePizzaSearchText, null);
            TakeOrderTheOrderPizzaSearchOnChanged(TheOrderPizzaSearchText, null);
        }

        private void RemovePizzaToOrder_Click(object sender, RoutedEventArgs e) {
            var SelectedItem = TheOrder_ListBox.SelectedItem;

            if (SelectedItem == null) {
                MessageBox.Show("Please select Pizza to remove from the order!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var piz = SelectedItem as Pizza;

            App.AppLocalStorage.AppTheOrder.PizzaList.Remove(piz);

            App.UpdatePizzaTotalPrice();
            TakeOrderTheOrderPizzaSearchOnChanged(TheOrderPizzaSearchText, null);
        }

        private void ClearPizzaOrder_Click(object sender, RoutedEventArgs e) {
            App.AppLocalStorage.AppTheOrder.PizzaList.Clear();

            App.UpdatePizzaTotalPrice();

            TakeOrderTheOrderPizzaSearchOnChanged(TheOrderPizzaSearchText, null);
        }

        private void Order_Click(object sender, RoutedEventArgs e) {
            if (App.AppLocalStorage.AppTheOrder.PizzaList.Count() == 0) {
                MessageBox.Show("No Pizza picked!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (App.AppLocalStorage.AppSetting.OrderMustHaveName) {
                if (App.AppLocalStorage.AppTheOrder.CustomerFirstName == "" && App.AppLocalStorage.AppTheOrder.CustomerLastName == "") {
                    MessageBox.Show("Please enter Customer Name!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            App.AppLocalStorage.AppTheOrder.ID = Helper.GetUnixMsID().ToString();
            App.AppLocalStorage.AppOrderList.Add(new Order(App.AppLocalStorage.AppTheOrder));
            //--

            if (App.AppLocalStorage.AppSetting.RefreshInfoAfterOrder) {
                App.AppLocalStorage.AppTheOrder = new Order(App.AppLocalStorage.AppSetting.TemplateOrder);

                CustomerInfo_Panel.DataContext = App.AppLocalStorage.AppTheOrder;
            }


            //--
            TakeOrderTheOrderPizzaSearchOnChanged(TheOrderPizzaSearchText, null);

            MessageBox.Show("Pizza ordered!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        private void TakeOrderPizzaSearchOnChanged(object sender, TextChangedEventArgs e) {
            var input = (sender as TextBox).Text.ToLower();

            var FilterResult = from obj in App.AppLocalStorage.AppPizzaList
                               where obj.Name.ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input) ||
                               obj.SizePriceUnit.ToString().ToLower().Contains(input) ||
                               obj.Size.ToString().ToLower().Contains(input)
                               select obj;

            Pizza_ListBox.ItemsSource = FilterResult;
        }

        private void TakeOrderTheOrderPizzaSearchOnChanged(object sender, TextChangedEventArgs e) {
            var input = (sender as TextBox).Text.ToLower();

            var FilterResult = from obj in App.AppLocalStorage.AppTheOrder.PizzaList
                               where obj.Name.ToLower().Contains(input) ||
                               obj.SizePriceUnit.ToString().ToLower().Contains(input) ||
                               obj.Size.ToString().ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input) ||
                               obj.Quantity.ToString().ToLower().Contains(input) ||
                               obj.TotalPrice.ToString().ToLower().Contains(input)
                               select obj;

            TheOrder_ListBox.ItemsSource = FilterResult;
        }

        DispatcherTimer dt = new DispatcherTimer();

        private void UpdateTime(object sender, EventArgs e) {
            App.AppLocalStorage.AppTheOrder.Date = DateTime.Now.ToString("dd-MM-yyyy");
            App.AppLocalStorage.AppTheOrder.Time = DateTime.Now.ToLongTimeString().ToString();
        }

        private void OnLoadEvent(object sender, RoutedEventArgs e) {
            Console.WriteLine("OnLoadEvent");
            //App.AppLocalStorage.AppTheOrder = new Order(App.AppLocalStorage.AppSetting.TemplateOrder);

            TheOrder_ListBox.ItemsSource = App.AppLocalStorage.AppTheOrder.PizzaList;
            CustomerInfo_Panel.DataContext = App.AppLocalStorage.AppTheOrder;

            Pizza_ListBox.ItemsSource = App.AppLocalStorage.AppPizzaList;

            PayStatusComboBox.ItemsSource = new List<string> { "Paied", "UnPay" };
            //------------
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += UpdateTime;
            dt.Start();
        }


        private void OnUnLoadEvent(object sender, RoutedEventArgs e) {
            dt.Stop();
        }
    }
}
