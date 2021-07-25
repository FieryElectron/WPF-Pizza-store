using PizzaStore.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using PizzaStore.Views;
using PizzaStore.ViewModels;
using System.Collections;
using System.Collections.ObjectModel;
using PizzaStore.Properties;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PizzaStore
{
    public partial class App : Application
    {
        
        public static string PresentWorkingDirectory;

        public static LocalStorage AppLocalStorage;

        private void ApplicationStartUp(object sender, StartupEventArgs e) {
            AppLocalStorage = new LocalStorage();

            PresentWorkingDirectory = Helper.GetPWDpath();

            Helper.LoadLocalStorage();
            Helper.MakeSureImageFoldersExist();
            Helper.PerformDeletion();
            
        }

        private void ApplicationExit(object sender, ExitEventArgs e) {
            Helper.SaveLocalStorage();
        }

        private void OrderPanel_MouseDown(object sender, MouseButtonEventArgs e) {
            StackPanel stackpanel = sender as StackPanel;

            if (stackpanel != null) {

                var p = stackpanel.FindName("PizzaListBox");
                if (p != null) {
                    var listbox = p as ListBox;

                    if (listbox.Visibility == Visibility.Visible) {
                        listbox.Visibility = Visibility.Collapsed;
                    } else {
                        listbox.Visibility = Visibility.Visible;
                    }

                }
            }
        }

        private void PizzaPanel_MouseDown(object sender, MouseButtonEventArgs e) {
            StackPanel stackpanel = sender as StackPanel;

            if (stackpanel != null) {
                var p = stackpanel.FindName("IngredientListBox");
                if (p != null) {
                    var listbox = p as ListBox;

                    if (listbox.Visibility == Visibility.Visible) {
                        listbox.Visibility = Visibility.Collapsed;
                    } else {
                        listbox.Visibility = Visibility.Visible;
                    }

                }
            }
        }

        private void UpdatePizzaTotalPriceLostFocus(object sender, RoutedEventArgs e) {
            float OrderPrice = 0.0f;
            foreach (var pizza in AppLocalStorage.AppTheOrder.PizzaList) {
                pizza.TotalPrice = pizza.Price * (float)pizza.Quantity;
                OrderPrice += pizza.TotalPrice;
            }
            AppLocalStorage.AppTheOrder.Price = OrderPrice;
        }

        public static void UpdatePizzaTotalPrice() {
            float OrderPrice = 0.0f;
            foreach (var pizza in AppLocalStorage.AppTheOrder.PizzaList) {
                pizza.TotalPrice = pizza.Price * (float)pizza.Quantity;
                OrderPrice += pizza.TotalPrice;
            }
            AppLocalStorage.AppTheOrder.Price = OrderPrice;
        }



    }
}
