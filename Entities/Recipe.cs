using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recipe
    {
        //Fields
        private List<Ingredient> ingredients;
        private string name;
        private int persons;
        private readonly int Id; //Skriver dette med stort I for at kunne skelne mellem id i constructoren


        //Properties
        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Persons
        {
            get { return persons; }
            set { persons = value; }
        }
        public int ID
        {
            get { return Id; }
        }


        public Recipe(string name, List<Ingredient> ingredients, int persons)
        {
            Name = name;
            Ingredients = ingredients;
            Persons = persons;
        }

        public Recipe(string name, List<Ingredient> ingredients, int persons, int id)
        {
            Name = name;
            Ingredients = ingredients;
            Persons = persons;
            Id = id;
        }

        //public List<IngredientType> GetIngredienTypes()
        //{

        //}

        public decimal GetPrice()
        {
            decimal totalPrice = 0;

            foreach (Ingredient ingredient in Ingredients)
            {
                totalPrice = totalPrice + ingredient.Price;
            }

            return totalPrice;
        }

        public override string ToString() //Skal måske have nogle ToStrings inde i {}'erne
        {
            return $"{Name} har nok mad til {Persons} personer, og koster {GetPrice()}kr. at lave";
        }
    }
}
