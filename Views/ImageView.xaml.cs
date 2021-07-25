using Microsoft.Win32;
using PizzaStore.Classes;
using System;
using System.Collections;
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


namespace PizzaStore.Views
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        public ImageView() {
            InitializeComponent();
        }

        private void OnLoadEvent(object sender, RoutedEventArgs e) {
            Image_PizzaImageListBox.ItemsSource = App.AppLocalStorage.AppPizzaImageList;
            Image_IngredientImageListBox.ItemsSource = App.AppLocalStorage.AppIngredientImageList;

            Helper.RefreshAppPizzaImageList();
            Helper.RefreshAppIngredientImageList();
        }


        private void Image_Pizza_Import_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {

                string destFile = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                destFile = destFile.Substring(0, destFile.LastIndexOf("\\")) + "\\" + App.AppLocalStorage.AppSetting.FileFolderForPizzaImage;

                string sourceFile = openFileDialog.FileName;
                string filename = sourceFile.Substring(sourceFile.LastIndexOf("\\"));

                Console.WriteLine(sourceFile);
                destFile = destFile + filename;
                Console.WriteLine(destFile);

                if (System.IO.File.Exists(destFile)) {
                    MessageBox.Show("File already exist!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else {
                    System.IO.File.Copy(sourceFile, destFile, true);

                    MessageBox.Show("Image Imported!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    Helper.RefreshAppPizzaImageList();

                    PizzaImageSearchOnChanged(PizzaImageSearchText, null);

                }

            }
        }

        private void Image_Pizza_Delete_Click(object sender, RoutedEventArgs e) {
            var record = Image_PizzaImageListBox.SelectedItem;

            if (record == null) {
                MessageBox.Show("Please select image first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                var toRemove = record as Img;

                var result = MessageBox.Show($"Sure about deleting Image: {toRemove.Name} ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK) {

                    App.AppLocalStorage.AppSetting.PendingDeleteImgList.Add(new Img(toRemove.Name, toRemove.ImagePath));
                    if (App.AppLocalStorage.AppSetting.ShowRestartNeededInfo) {
                        MessageBox.Show("Restart App required! Disable the warning in the setting!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    PizzaImageSearchOnChanged(PizzaImageSearchText, null);

                }

            }
        }

        private void Image_Ingredient_Import_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {

                string destFile = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                destFile = destFile.Substring(0, destFile.LastIndexOf("\\")) + "\\" + App.AppLocalStorage.AppSetting.FileFolderForIngredientImage;

                string sourceFile = openFileDialog.FileName;
                string filename = sourceFile.Substring(sourceFile.LastIndexOf("\\"));

                Console.WriteLine(sourceFile);
                destFile = destFile + filename;
                Console.WriteLine(destFile);

                if (System.IO.File.Exists(destFile)) {
                    MessageBox.Show("File already exist!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else {
                    System.IO.File.Copy(sourceFile, destFile, true);

                    MessageBox.Show("Image Imported!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    Helper.RefreshAppIngredientImageList();

                    IngredientImageSearchOnChanged(IngredientImageSearchText, null);
                }

            }
        }

        private void Image_Ingredient_Delete_Click(object sender, RoutedEventArgs e) {
            var record = Image_IngredientImageListBox.SelectedItem;

            if (record == null) {
                MessageBox.Show("Please select image first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                var toRemove = record as Img;

                var result = MessageBox.Show($"Sure about deleting Image: {toRemove.Name} ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK) {

                    App.AppLocalStorage.AppSetting.PendingDeleteImgList.Add(new Img(toRemove.Name, toRemove.ImagePath));

                    if (App.AppLocalStorage.AppSetting.ShowRestartNeededInfo) {
                        MessageBox.Show("Restart App required! Disable the warning in the setting!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    IngredientImageSearchOnChanged(IngredientImageSearchText, null);

                }

            }
        }

        



        private void PizzaImageSearchOnChanged(object sender, TextChangedEventArgs e) {
            var FilterResult = from obj in App.AppLocalStorage.AppPizzaImageList
                               where obj.Name.ToLower().Contains((sender as TextBox).Text.ToLower())
                               select obj;

            Image_PizzaImageListBox.ItemsSource = FilterResult;
        }

        private void IngredientImageSearchOnChanged(object sender, TextChangedEventArgs e) {
            var FilterResult = from obj in App.AppLocalStorage.AppIngredientImageList
                               where obj.Name.ToLower().Contains((sender as TextBox).Text.ToLower())
                               select obj;

            Image_IngredientImageListBox.ItemsSource = FilterResult;
        }


    }
}
