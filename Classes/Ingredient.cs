using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Classes
{
    public class Ingredient : INotifyPropertyChanged {
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

        float _price;
        public float Price {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
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

        public Ingredient() {
            Name = "Ingredient";
            Price = 1.0f;
            ImagePath = "";
            Quantity = 1;
        }

        public Ingredient(Ingredient ingredient) {
            Name = ingredient.Name;
            Price = ingredient.Price;
            ImagePath = ingredient.ImagePath;
            Quantity = ingredient.Quantity;
        }



    }
}
