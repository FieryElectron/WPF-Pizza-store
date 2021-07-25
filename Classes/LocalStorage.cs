using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace PizzaStore.Classes
{
    

    public class LocalStorage {
        public static string FilePath = "LocalStorage.txt";

        public  Setting AppSetting = new Setting();

        public  Order AppTheOrder { get; set; }

        public  ObservableCollection<Img> AppIngredientImageList = new ObservableCollection<Img>();

        public  ObservableCollection<Img> AppPizzaImageList = new ObservableCollection<Img>();

        public  ObservableCollection<Ingredient> AppIngredientList = new ObservableCollection<Ingredient>();

        public  ObservableCollection<Pizza> AppPizzaList = new ObservableCollection<Pizza>();

        public  ObservableCollection<Order> AppOrderList = new ObservableCollection<Order>();

        public LocalStorage() {
            AppTheOrder = new Order();
        }

    }
}
