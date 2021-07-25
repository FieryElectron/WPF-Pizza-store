using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PizzaStore.Classes
{
    public class Pizza : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        string _name;
        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        float _sizePriceUnit;
        public float SizePriceUnit {
            get { return _sizePriceUnit; }
            set { _sizePriceUnit = value; OnPropertyChanged("SizePriceUnit"); }
        }

        uint _size;
        public uint Size {
            get { return _size; }
            set { _size = value; OnPropertyChanged("Size"); }
        }

        float _price;
        public float Price {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        float _totalPrice;
        public float TotalPrice {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged("TotalPrice"); }
        }

        string _imagePath;
        public string ImagePath {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }

        uint _quantity;
        public uint Quantity {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged("Quantity"); }
        }

        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public Pizza() {
            Name = "Pizza";
            SizePriceUnit = 1.0f;
            Size = 7;
            Price = SizePriceUnit * (float)Size;
            TotalPrice = Price;
            ImagePath = "";
            Quantity = 1;
            Ingredients = new ObservableCollection<Ingredient>();
        }

        public Pizza(Pizza pizza) {
            Name = pizza.Name;
            SizePriceUnit = pizza.SizePriceUnit;
            Size = pizza.Size;
            Price = pizza.Price;
            TotalPrice = pizza.TotalPrice;
            ImagePath = pizza.ImagePath;
            Quantity = pizza.Quantity;
            Ingredients = new ObservableCollection<Ingredient>();

            foreach (var Ingredient in pizza.Ingredients) {
                Ingredients.Add(Ingredient);
            }
        }

    }
}
