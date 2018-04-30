using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DbAccess
{
    public class DBHandler : CommonDB
    {
        public DBHandler(string conString) : base (conString) //Virker dette ordenligt?
        {
            
        }

        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            DataSet dataSet = ExecuteQuery("SELECT * FROM Ingredients");
            DataRow[] dataRow = dataSet.Tables[0].Select();

            foreach (DataRow row in dataRow)
            {
                Ingredient ingredient = new Ingredient(
                    row.Field<decimal>("IngredientPrice"),
                    row.Field<string>("IngredientName"),
                    (IngredientType)Enum.Parse(typeof(IngredientType), row.Field<string>("IngredientType").ToString()),
                    row.Field<int>("IngredientID")
                );

                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        //public List<Recipe> GetAllRecipies()
        //{

        //}

        //public Ingredient GetIngredientByName(string name)
        //{

        //}

        //public Recipe GetRecipeByName(string name)
        //{

        //}
    }
}
