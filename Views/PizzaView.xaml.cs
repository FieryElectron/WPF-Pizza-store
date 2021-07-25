using PizzaStore.Classes;
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
using System.Windows.Threading;

namespace PizzaStore.Views
{
    /// <summary>
    /// Interaction logic for PizzaView.xaml
    /// </summary>
    public partial class PizzaView : UserControl
    {
        public PizzaView() {
            InitializeComponent();
        }

        private void OnLoadEvent(object sender, RoutedEventArgs e) {
            Pizza_PizzaListBox.ItemsSource = App.AppLocalStorage.AppPizzaList;

            Pizza_AvailableIngredientListBox.ItemsSource = App.AppLocalStorage.AppIngredientList;

            Pizza_PizzaImageListBox.ItemsSource = App.AppLocalStorage.AppPizzaImageList;

            Helper.RefreshAppPizzaImageList();
        }

        private void AddNewIngredient_Click(object sender, RoutedEventArgs e) {
            var selectedIngredient = Pizza_AvailableIngredientListBox.SelectedItem as Ingredient;
            if (selectedIngredient == null) {
                MessageBox.Show("Please select new Ingredient first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            var selectedPizza = Pizza_PizzaListBox.SelectedItem as Pizza;
            if (selectedPizza == null) {
                MessageBox.Show("Please select Pizza first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedPizza.Ingredients.Add(new Ingredient { Name = selectedIngredient.Name, Price = selectedIngredient.Price, ImagePath = selectedIngredient.ImagePath, Quantity = selectedIngredient.Quantity });
            //----

            var selected = Pizza_PizzaListBox.SelectedItem;
            if (selected == null) {
                return;
            }

            var pizza = selected as Pizza;
            if (pizza == null) {
                return;
            }

            var FilterResult = from obj in pizza.Ingredients
                               where obj.Name.ToLower().Contains(CurrentFilterText.Text.ToLower())
                               select obj;

            Pizza_IngredientListBox.ItemsSource = FilterResult;

            UpdatePizzaPriceLostFocus(null, null);

            PizzaCurrentIngredientSearchOnChanged(CurrentFilterText, null);

        }




        private void Pizza_Ingredient_Delete_Click(object sender, RoutedEventArgs e) {

            var selectedIngredient = Pizza_IngredientListBox.SelectedItem as Ingredient;
            if (selectedIngredient == null) {
                MessageBox.Show("Please select current Ingredient first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            var selectedPizza = Pizza_PizzaListBox.SelectedItem as Pizza;
            if (selectedPizza == null) {
                MessageBox.Show("Please select Pizza first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedPizza.Ingredients.Remove(selectedIngredient);
            //--
            //Pizza_IngredientListBox.ItemsSource = null;

            //UpdatePizzaPriceLostFocus(null, null);
            var selected = Pizza_PizzaListBox.SelectedItem;
            if (selected == null) {
                return;
            }

            var pizza = selected as Pizza;
            if (pizza == null) {
                return;
            }

            var FilterResult = from obj in pizza.Ingredients
                               where obj.Name.ToLower().Contains(CurrentFilterText.Text.ToLower())
                               select obj;

            Pizza_IngredientListBox.ItemsSource = FilterResult;

            UpdatePizzaPriceLostFocus(null, null);
            PizzaCurrentIngredientSearchOnChanged(CurrentFilterText, null);
        }

        private void Pizza_Delete_Click(object sender, RoutedEventArgs e) {
            var record = Pizza_PizzaListBox.SelectedItem;

            if (record == null) {
                MessageBox.Show("Please select pizza first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                var toRemove = record as Pizza;

                var result = MessageBox.Show($"Sure about deleting Pizza: {toRemove.Name} ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK) {
                    App.AppLocalStorage.AppPizzaList.Remove(toRemove);
                    PizzaSearchOnChanged(PizzaSearchText, null);


                }

            }
        }

        private void Pizza_New_Click(object sender, RoutedEventArgs e) {
            var pizza = new Pizza ( App.AppLocalStorage.AppSetting.TemplatePizza);
            App.AppLocalStorage.AppPizzaList.Add(pizza);

            PizzaSearchOnChanged(PizzaSearchText, null);

        }



        private void Pizza_ImageModify_Click(object sender, RoutedEventArgs e) {
            var record = Pizza_PizzaListBox.SelectedItem;

            Pizza toModify;

            if (record == null) {
                MessageBox.Show("Please select Pizza first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else {
                toModify = record as Pizza;
            }

            var img = Pizza_PizzaImageListBox.SelectedItem;

            if (img == null) {
                MessageBox.Show("Plz select Image!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var image = img as Img;

            var result = MessageBox.Show($"Sure about Modifiy Image to {image.Name} ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK) {
                toModify.ImagePath = image.ImagePath;
                MessageBox.Show("Modification successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void PizzaSearchOnChanged(object sender, TextChangedEventArgs e) {
            var input = (sender as TextBox).Text.ToLower();

            var FilterResult = from obj in App.AppLocalStorage.AppPizzaList
                               where obj.Name.ToLower().Contains(input) ||
                               obj.SizePriceUnit.ToString().ToLower().Contains(input) ||
                               obj.Size.ToString().ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input)
                               select obj;

            Pizza_PizzaListBox.ItemsSource = FilterResult;
        }

        private void PizzaImageSearchOnChanged(object sender, TextChangedEventArgs e) {
            var FilterResult = from obj in App.AppLocalStorage.AppPizzaImageList
                               where obj.Name.ToLower().Contains((sender as TextBox).Text.ToLower())
                               select obj;

            Pizza_PizzaImageListBox.ItemsSource = FilterResult;
        }

        private void PizzaIngredientSearchOnChanged(object sender, TextChangedEventArgs e) {
            var input = (sender as TextBox).Text.ToLower();

            var FilterResult = from obj in App.AppLocalStorage.AppIngredientList
                               where obj.Name.ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input) ||
                               obj.Quantity.ToString().ToLower().Contains(input)
                               select obj;

            Pizza_AvailableIngredientListBox.ItemsSource = FilterResult;
        }

        private void PizzaCurrentIngredientSearchOnChanged(object sender, TextChangedEventArgs e) {
            var selected = Pizza_PizzaListBox.SelectedItem;
            if (selected == null) {
                return;
            }

            var input = (sender as TextBox).Text.ToLower();
            var pizza = selected as Pizza;

            var FilterResult = from obj in pizza.Ingredients
                               where obj.Name.ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input) ||
                               obj.Quantity.ToString().ToLower().Contains(input)
                               select obj;

            Pizza_IngredientListBox.ItemsSource = FilterResult;
        }

        private void PizzaSelectionChanged(object sender, SelectionChangedEventArgs e) {
            ////var select = Pizza_PizzaListBox.SelectedItem;
            ////if (select == null) {
            ////    Pizza_IngredientListBox.ItemsSource = null;
            ////    return;
            ////}
            ////Pizza_IngredientListBox.ItemsSource = (select as Pizza).Ingredients;
            var selected = Pizza_PizzaListBox.SelectedItem;
            if (selected == null) {
                return;
            }

            var pizza = selected as Pizza;
            if (pizza == null) {
                return;
            }

            var FilterResult = from obj in pizza.Ingredients
                               where obj.Name.ToLower().Contains(CurrentFilterText.Text.ToLower())
                               select obj;

            Pizza_IngredientListBox.ItemsSource = FilterResult;


            PizzaCurrentIngredientSearchOnChanged(CurrentFilterText, null);

        }

        private void TimerUpdatePizzaPriceLostFocus(object sender, EventArgs e) {
            (sender as DispatcherTimer).Stop();
            var pizza = Pizza_PizzaListBox.SelectedItem as Pizza;
            Helper.UpdatePizzaPrice(pizza);
        }

        private void UpdatePizzaPriceLostFocus(object sender, RoutedEventArgs e) {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1);
            dt.Tick += TimerUpdatePizzaPriceLostFocus;
            dt.Start();
        }

        //---
        private void PriceOnChanged(object sender, TextChangedEventArgs e) {
            var select = Pizza_PizzaListBox.SelectedItem;
            if (select == null) {
                return;
            }
            var pizza = select as Pizza;
            var txt = (sender as TextBox).Text;

            if (!Helper.PriceCheck(txt)) {
                MessageBox.Show("Positive value Only!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                pizza.SizePriceUnit = 0;
            }
        }


    }
}
