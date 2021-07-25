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
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Pizza> PizzaList { get; set; }

        string _iD;
        public string ID {
            get { return _iD; }
            set { _iD = value; OnPropertyChanged("ID"); }
        }


        string _customerFirstName;
        public string CustomerFirstName {
            get { return _customerFirstName; }
            set { _customerFirstName = value; OnPropertyChanged("CustomerFirstName"); }
        }

        string _customerLastName;
        public string CustomerLastName {
            get { return _customerLastName; }
            set { _customerLastName = value; OnPropertyChanged("CustomerLastName"); }
        }

        string _customerPhone;
        public string CustomerPhone {
            get { return _customerPhone; }
            set { _customerPhone = value; OnPropertyChanged("CustomerPhone"); }
        }

        string _customerEmail;
        public string CustomerEmail {
            get { return _customerEmail; }
            set { _customerEmail = value; OnPropertyChanged("CustomerEmail"); }
        }

        string _customerAddress;
        public string CustomerAddress {
            get { return _customerAddress; }
            set { _customerAddress = value; OnPropertyChanged("CustomerAddress"); }
        }

        string _date;
        public string Date {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }

        string _time;
        public string Time {
            get { return _time; }
            set { _time = value; OnPropertyChanged("Time"); }
        }

        float _price;
        public float Price {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        bool _payStatus;
        public bool PayStatus {
            get { return _payStatus; }
            set { _payStatus = value; OnPropertyChanged("PayStatus"); }
        }



        public Order() {
            ID = Helper.GetUnixMsID().ToString();
            CustomerFirstName = "FirstName";
            CustomerLastName = "LastName";
            CustomerPhone = "+...";
            CustomerEmail = "...@...";
            CustomerAddress = "Str...";
            Date = DateTime.Now.ToString("dd-MM-yyyy");
            Time = DateTime.Now.ToLongTimeString().ToString();
            Price = 0.0f;
            PayStatus = false;

            PizzaList = new ObservableCollection<Pizza>();
        }

        public Order(Order order) {
            ID = Helper.GetUnixMsID().ToString();
            CustomerFirstName = order.CustomerFirstName;
            CustomerLastName = order.CustomerLastName;
            CustomerPhone = order.CustomerPhone;
            CustomerEmail = order.CustomerEmail;
            CustomerAddress = order.CustomerAddress;
            Date = DateTime.Now.ToString("dd-MM-yyyy");
            Time = DateTime.Now.ToLongTimeString().ToString();
            Price = order.Price;
            PayStatus = order.PayStatus;

            PizzaList = new ObservableCollection<Pizza>();

            foreach (var pizza in order.PizzaList) {
                PizzaList.Add(new Pizza(pizza));
            }
        }
    }
}
