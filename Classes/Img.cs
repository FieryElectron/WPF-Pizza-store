using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Classes
{
    public class Img
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public Img(string name, string imagePath) {
            Name = name;
            ImagePath = imagePath;
        }

    }
}
