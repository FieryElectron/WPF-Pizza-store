using Microsoft.Win32;
using PizzaStore.Classes;
using PizzaStore.ViewModels;
using PizzaStore.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PizzaStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string language;

        bool firstSetting = true; 

        public MainWindow() {
            language = Properties.Settings.Default.language;

            CultureInfo.CurrentCulture = new CultureInfo(language);
            CultureInfo.CurrentUICulture = new CultureInfo(language);

            InitializeComponent();
            Language_Combox.ItemsSource = new List<string> { "en English", "zh 中文" };

            if (language == "en") {
                Language_Combox.SelectedItem = "en English";
            } else {
                Language_Combox.SelectedItem = "zh 中文";
            }
            firstSetting = false;


        }

        public static List<Button> ButtonList = new List<Button>();

        private string buttonTextPressed;
        private void SetPressedButtonColor(Button _button) {
            foreach (var button in ButtonList) {
                if (button == _button) {
                    button.Background = Brushes.White;
                } else {
                    button.Background = Brushes.Gray;
                }
            }
        }

        private void SetEnableAllButtons(bool bo) {
            foreach (var button in ButtonList) {
                button.IsEnabled = bo;
            }
        }


        private void OnLoadEvent(object sender, RoutedEventArgs e) {

            ButtonList.Add(WelcomeButton);
            ButtonList.Add(TakeOrderButton);
            ButtonList.Add(OrderButton);
            ButtonList.Add(PizzaButton);
            ButtonList.Add(IngredientButton);
            ButtonList.Add(ImageButton);
            ButtonList.Add(SettingButton);
            ButtonList.Add(AboutButton);

            CurrentContentControl = ContentControl1 as ContentControl;
            PendingContentControl = ContentControl2 as ContentControl;
            
            if (App.AppLocalStorage.AppSetting.ShowWelcomeOnStartUp) {
                SwitchView_Clicked(WelcomeButton,null);
            } else {
                SwitchView_Clicked(TakeOrderButton, null);
            }

        }

        private void ReEnableButtons(object sender, EventArgs e) {
            (sender as DispatcherTimer).Stop();
            //SetEnableAllButtons(true);
            CurrentContentControl.DataContext = null;
            
        }

        ContentControl PendingContentControl;
        ContentControl CurrentContentControl;

        public void SwitchView_Clicked(object sender, RoutedEventArgs e) {
            var btn = sender as Button;

            string Text = btn.Content.ToString();

            if (buttonTextPressed == Text) {
                return;
            }

            //SetEnableAllButtons(false);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += ReEnableButtons;
            dt.Start();

            SetPressedButtonColor(btn);
  
            buttonTextPressed = Text;

            ContentControl tmp = PendingContentControl;
            PendingContentControl = CurrentContentControl;
            CurrentContentControl = tmp;

            switch (Text) {
                case "Welcome":
                case "欢迎":
                    PendingContentControl.DataContext = new WelcomeModel();
                    break;
                case "TakeOrder":
                case "点单":
                    PendingContentControl.DataContext = new TakeOrderModel();
                    break;
                case "Order":
                case "订购":
                    PendingContentControl.DataContext = new OrderModel();
                    break;
                case "Pizza":
                case "披萨":
                    PendingContentControl.DataContext = new PizzaModel();
                    break;
                case "Ingredient":
                case "原料":
                    PendingContentControl.DataContext = new IngredientModel();
                    break;
                case "Image":
                case "图片":
                    PendingContentControl.DataContext = new ImageModel();
                    break;
                case "Setting":
                case "设置":
                    PendingContentControl.DataContext = new SettingModel();
                    break;
                case "About":
                case "关于":

                    break;
                default:
                    Console.WriteLine("Button Text Wrong!");
                    break;
            }

            DoubleAnimation AnimL = new DoubleAnimation();
            DoubleAnimation AnimR = new DoubleAnimation();

            AnimL.From = -1000;
            AnimL.To = 0;
            AnimL.EasingFunction = new QuarticEase();
            AnimL.Duration = TimeSpan.FromSeconds(1);

            AnimR.From = 0;
            AnimR.To = 1000;
            AnimR.EasingFunction = new QuarticEase();
            AnimR.Duration = TimeSpan.FromSeconds(1);

            PendingContentControl.BeginAnimation(Canvas.LeftProperty, AnimL);
            CurrentContentControl.BeginAnimation(Canvas.LeftProperty, AnimR);



        }

        private void AboutView_Clicked(object sender, RoutedEventArgs e) {
            var win = new AboutWindow();
            win.Owner = this;

            win.ShowDialog();
        }

        private void Language_Combox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            language = Language_Combox.SelectedItem.ToString().Substring(0,2);

            Properties.Settings.Default.language = language;
            Properties.Settings.Default.Save();

            if (firstSetting) {
                return;
            }

            if (App.AppLocalStorage.AppSetting.ShowRestartNeededInfo) {
                MessageBox.Show("Restart App required! Disable the warning in the setting!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
