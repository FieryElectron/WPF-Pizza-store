using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace PizzaStore.Classes
{
    public static class Helper
    {
        public static void LoadLocalStorage() {
            var loaded = LoadClass(typeof(LocalStorage), LocalStorage.FilePath) as LocalStorage;

            if (loaded != null) {
                Console.WriteLine("LocalStorage Loaded!");
                App.AppLocalStorage = loaded;
            } else {
                Console.WriteLine("LocalStorage Load failed!");
            }
        }

        public static void SaveLocalStorage() {
            var p = new LocalStorage();
            p = App.AppLocalStorage;
            SaveClass(p, LocalStorage.FilePath);
            Console.WriteLine("LocalStorage Saved!");
        }
     
        public static void SaveClass(object data, string filepath) {
            JsonSerializer js = new JsonSerializer();

            if (File.Exists(filepath)) {
                File.Delete(filepath);
            }

            StreamWriter sw = new StreamWriter(filepath);
            JsonWriter jw = new JsonTextWriter(sw);

            js.Serialize(jw, data);

            jw.Close();
            sw.Close();
        }

        public static object LoadClass(Type dataType, string filepath) {
            if (File.Exists(filepath)) {
                JsonSerializer jsonSerializer = new JsonSerializer();
                StreamReader sr = new StreamReader(filepath);
                JsonReader jsonReader = new JsonTextReader(sr);

                JObject obj = jsonSerializer.Deserialize(jsonReader) as JObject;
                jsonReader.Close();
                sr.Close();

                return obj.ToObject(dataType);
            }

            return null;
        }

        //--------------------

        public static long GetUnixMsID() {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }

        public static bool SizeCheck(string str) {
            if (str.Length == 0) {
                return false;
            }
            bool rex = Regex.IsMatch(str, "^([0-9]{1,})$");
            return rex;
        }

        public static bool PriceCheck(string str) {
            if (str.Length == 0) {
                return false;
            }
            bool rez = Regex.IsMatch(str, "^([0-9]{1,}[.][0-9]*)$");
            bool rex = Regex.IsMatch(str, "^([0-9]{1,})$");
            return rez || rex;
        }

        public static bool NameCheck(string str) {
            if (str.Length == 0) {
                return false;
            }
            bool res = Regex.IsMatch(str, "^[A-Za-z0-9]+$");
            return res;
        }

        public static bool ImgPathCheck(string str) {
            if (str.Length == 0) {
                return false;
            }
            if (System.IO.File.Exists(str)) {
                return true;
            } else {
                return false;
            }
        }

        public static void MakeSureImageFoldersExist() {
            if (!System.IO.Directory.Exists(App.AppLocalStorage.AppSetting.FileFolderForIngredientImage)) {
                System.IO.Directory.CreateDirectory(App.AppLocalStorage.AppSetting.FileFolderForIngredientImage);
            }

            if (!System.IO.Directory.Exists(App.AppLocalStorage.AppSetting.FileFolderForPizzaImage)) {
                System.IO.Directory.CreateDirectory(App.AppLocalStorage.AppSetting.FileFolderForPizzaImage);
            }
        }

        public static void PerformDeletion() {
            foreach (Img image in App.AppLocalStorage.AppSetting.PendingDeleteImgList) {
                if (System.IO.File.Exists(image.ImagePath)) {
                    try {
                        System.IO.File.Delete(image.ImagePath);
                        Console.WriteLine("File: " + image.ImagePath + " Deleted!");
                    } catch (System.IO.IOException ee) {
                        Console.WriteLine(ee.Message);
                        return;
                    }
                }
            }
            App.AppLocalStorage.AppSetting.PendingDeleteImgList.Clear();

        }

        public static string GetPWDpath() {
            string PresentWorkingDirectory = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            PresentWorkingDirectory = PresentWorkingDirectory.Substring(0, PresentWorkingDirectory.LastIndexOf("\\") + 1);

            return PresentWorkingDirectory;
        }

        public static void RefreshAppIngredientImageList() {
            string FolderPath = App.PresentWorkingDirectory + App.AppLocalStorage.AppSetting.FileFolderForIngredientImage;

            if (FolderPath != "") {
                App.AppLocalStorage.AppIngredientImageList.Clear();

                DirectoryInfo folder = new DirectoryInfo(FolderPath);

                if (folder.Exists) {

                    foreach (FileInfo fileInfo in folder.GetFiles()) {
                        if (".jpg|.jpeg|.png".Contains(fileInfo.Extension.ToLower())) {
                            App.AppLocalStorage.AppIngredientImageList.Add(new Img(fileInfo.Name, fileInfo.FullName));
                        }
                    }

                }
            }

        }

        public static void RefreshAppPizzaImageList() {
            string FolderPath = App.PresentWorkingDirectory + App.AppLocalStorage.AppSetting.FileFolderForPizzaImage;

            if (FolderPath != "") {
                App.AppLocalStorage.AppPizzaImageList.Clear();

                DirectoryInfo folder = new DirectoryInfo(FolderPath);

                if (folder.Exists) {

                    foreach (FileInfo fileInfo in folder.GetFiles()) {
                        if (".jpg|.jpeg|.png".Contains(fileInfo.Extension.ToLower())) {
                            App.AppLocalStorage.AppPizzaImageList.Add(new Img(fileInfo.Name, fileInfo.FullName));
                        }
                    }

                }
            }

        }

        public static void setButtonEnable(object sender, EventArgs e, Button btn, bool enable) {
            Console.WriteLine("setButtonEnable");
            if (sender != null) {
                (sender as DispatcherTimer).Stop();
            }

            if (btn != null) {
                btn.IsEnabled = enable;
            }
        }

        public static void UpdatePizzaPrice(Pizza pizza) {

            if (pizza == null) {
                Console.WriteLine("Null");
                return;
            }
            float perPrice = 0.0f;

            perPrice += pizza.SizePriceUnit * (float)pizza.Size;

            foreach (var ingredient in pizza.Ingredients) {
                perPrice += ingredient.Price * (float)ingredient.Quantity;
            }

            pizza.Price = perPrice;

            pizza.TotalPrice = perPrice * (float)pizza.Quantity;




        }

        public static string ConvetBoolToStringPayStatue(bool flag) {
            if (flag) {
                return "Paied";
            } else {
                return "UnPay";
            }
        }
    }

    
}
