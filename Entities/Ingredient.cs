using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Ingredient
    {
        //Fields
        private readonly int ID; //Skriver dette med uppercase for at kunne skelne mellem ID og id i constructoren
        private IngredientType type;
        private string name;
        private decimal price;


        //Properties
        public IngredientType Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        } 


        public Ingredient(decimal price, string name, IngredientType type)
        {
            Price = price;
            Name = name;
            Type = type;
        }

        public Ingredient(decimal price, string name, IngredientType type, int id)
        {
            Price = price;
            Name = name;
            Type = type;
            ID = id;
        }

        public override string ToString()
        {
            return $"{Name} er en {Type} og koster {Price}kr. pr. kg.";
        }
    }
}
