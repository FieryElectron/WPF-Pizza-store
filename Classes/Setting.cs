using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;

namespace PizzaStore.Classes
{
    public class Setting
    {

        public bool ShowWelcomeOnStartUp { get; set; }

        public bool ShowRestartNeededInfo { get; set; }

        public bool OrderMustHaveName { get; set; }

        public bool RefreshInfoAfterOrder { get; set; }

        public string FileFolderForIngredientImage { get; set; } 

        public string FileFolderForPizzaImage { get; set; }

        public ObservableCollection<Img> PendingDeleteImgList { get; set; }

        public Order TemplateOrder { get; set; }

        public Pizza TemplatePizza { get; set; }

        public Ingredient TemplateIngredient { get; set; }

        public Setting() {
            ShowWelcomeOnStartUp = true;
            ShowRestartNeededInfo = true;
            OrderMustHaveName = true;
            RefreshInfoAfterOrder = true;

            FileFolderForIngredientImage = "IngredientImages";
            FileFolderForPizzaImage = "PizzaImages";

            TemplateIngredient = new Ingredient { Name= "Ingredient",Price = 1.0f};
            TemplatePizza = new Pizza();
            TemplateOrder = new Order();

            PendingDeleteImgList = new ObservableCollection<Img>();
        }

    }
}
