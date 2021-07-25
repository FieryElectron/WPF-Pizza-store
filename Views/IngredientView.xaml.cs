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

namespace PizzaStore.Views
{
    /// <summary>
    /// Interaction logic for IngredientView.xaml
    /// </summary>
    public partial class IngredientView : UserControl
    {
        public IngredientView() {
            InitializeComponent();
        }

        private void Ingredient_Delete_Click(object sender, RoutedEventArgs e) {
            var record = Ingredient_IngredientListBox.SelectedItem;

            if (record == null) {
                MessageBox.Show("Please select ingredient first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                var toRemove = record as Ingredient;

                var result = MessageBox.Show($"Sure about deleting Ingredient: {toRemove.Name} {toRemove.Name}?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK) {
                    App.AppLocalStorage.AppIngredientList.Remove(toRemove);

                    IngredientSearchOnChanged(IngredientSearchText, null);
                }

            }
        }

        private void OnLoadEvent(object sender, RoutedEventArgs e) {
            Ingredient_IngredientListBox.ItemsSource = App.AppLocalStorage.AppIngredientList;
            Helper.RefreshAppIngredientImageList();
            Ingredient_ImageListBox.ItemsSource = App.AppLocalStorage.AppIngredientImageList;
        }

        private void Modify_Image_Click(object sender, RoutedEventArgs e) {

            var record = Ingredient_IngredientListBox.SelectedItem;

            Ingredient toModify;

            if (record == null) {
                MessageBox.Show("Please select ingredient first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else {
                toModify = record as Ingredient;
            }



            //---
            var img = Ingredient_ImageListBox.SelectedItem;

            if (img == null) {
                MessageBox.Show("Please select Image!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var image = img as Img;
            //---

            var result = MessageBox.Show($"Sure about Modifiy Image ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK) {

                toModify.ImagePath = image.ImagePath;
                MessageBox.Show("Modification successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        
        private void Ingredient_New_Click(object sender, RoutedEventArgs e) {
            var template = App.AppLocalStorage.AppSetting.TemplateIngredient;
            var ingredient = new Ingredient { Name = template.Name, Price = template .Price};

            App.AppLocalStorage.AppIngredientList.Add(ingredient);

            IngredientSearchOnChanged(IngredientSearchText, null);
      
        }

        private void IngredientSearchOnChanged(object sender, TextChangedEventArgs e) {
            var input = (sender as TextBox).Text.ToLower();
            var FilterResult = from obj in App.AppLocalStorage.AppIngredientList
                               where obj.Name.ToLower().Contains(input) ||
                               obj.Price.ToString().ToLower().Contains(input)
                               select obj;

            Ingredient_IngredientListBox.ItemsSource = FilterResult;
        }

        private void IngredientImageSearchOnChanged(object sender, TextChangedEventArgs e) {
            var FilterResult = from obj in App.AppLocalStorage.AppIngredientImageList
                               where obj.Name.ToLower().Contains((sender as TextBox).Text.ToLower())
                               select obj;

            Ingredient_ImageListBox.ItemsSource = FilterResult;
        }

        private void PriceOnChanged(object sender, TextChangedEventArgs e) {
            var select = Ingredient_IngredientListBox.SelectedItem;
            if (select == null) {
                return;
            }
            var ingredient = select as Ingredient;
            var txt = (sender as TextBox).Text;

            if (!Helper.PriceCheck(txt)) {
                MessageBox.Show("Positive value Only!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                ingredient.Price = 0;
            }
        }
    }
}
