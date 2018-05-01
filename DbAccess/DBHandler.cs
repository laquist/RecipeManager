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

            DataSet dataSet = ExecuteQuery("SELECT * FROM Ingredients;");
            DataRow[] dataRow = dataSet.Tables[0].Select();

            foreach (DataRow row in dataRow)
            {
                Ingredient ingredient = new Ingredient(
                    Convert.ToDecimal(row.Field<int>("IngredientPrice")),
                    row.Field<string>("IngredientName"),
                    (IngredientType)Enum.Parse(typeof(IngredientType), row.Field<string>("IngredientType").ToString()),
                    row.Field<int>("IngredientID")
                );

                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public List<Recipe> GetAllRecipies()
        {
            //Ingredients og RecipeVsIngredient
            DataSet recipeVsIngredientDataSet = ExecuteQuery("SELECT * FROM RecipeVsIngredient;");
            DataRow[] recipeVsIngredientDataRow = recipeVsIngredientDataSet.Tables[0].Select();

            List<Ingredient> allIngredients = GetAllIngredients();

            //Recipes
            DataSet recipeDataSet = ExecuteQuery("SELECT * FROM Recipes;");
            DataRow[] recipeDataRow = recipeDataSet.Tables[0].Select();

            List<Recipe> recipes = new List<Recipe>();

            foreach (DataRow row in recipeDataRow)
            {
                List<Ingredient> ingredientsInRecipe = new List<Ingredient>();

                foreach (DataRow recipeVsIngredientRow in recipeVsIngredientDataRow)
                {
                    if (row.Field<int>("RecipeID") == recipeVsIngredientRow.Field<int>("RecipeID"))
                    {
                        int ingredientID = recipeVsIngredientRow.Field<int>("IngredientID");

                        foreach (Ingredient ingredient in allIngredients)
                        {
                            if (ingredient.ID == ingredientID)
                            {
                                ingredientsInRecipe.Add(ingredient);
                            }
                        }
                    }
                }

                Recipe recipe = new Recipe(
                    row.Field<string>("RecipeName"),
                    ingredientsInRecipe,
                    row.Field<int>("RecipePersons"),
                    row.Field<int>("RecipeID")
                );

                recipes.Add(recipe);
            }

            return recipes;
        }

        public Ingredient GetIngredientByName(string name) //Kunne også laves ved at bruge GetAllIngredients() metoden og så bare køre alle ingredients igennem. Men dette er mindre data og mindre trafik til DB'en
        {
            DataSet dataSet = ExecuteQuery($"SELECT * FROM Ingredients WHERE IngredientName='{name}';");
            DataRow[] dataRow = dataSet.Tables[0].Select();
            DataRow row = dataRow[0];

            Ingredient ingredient = new Ingredient(
                row.Field<decimal>("IngredientPrice"),
                row.Field<string>("IngredientName"),
                (IngredientType)Enum.Parse(typeof(IngredientType), row.Field<string>("IngredientType").ToString()),
                row.Field<int>("IngredientID")
            );

            return ingredient;
        }

        public Recipe GetRecipeByName(string name) //Dette er et eksempel på en anden måde at gøre det på. Ved at bruge GetAllRecipes() metoden og så køre den igennem for at finde ud af den Recipe der matcher med {name}
        {
            //Ingredients og RecipeVsIngredient
            DataSet recipeVsIngredientDataSet = ExecuteQuery("SELECT * FROM RecipeVsIngredient;");
            DataRow[] recipeVsIngredientDataRow = recipeVsIngredientDataSet.Tables[0].Select();

            List<Ingredient> allIngredients = GetAllIngredients();
            List<Ingredient> ingredientsInRecipe = new List<Ingredient>();

            //Recipe
            DataSet dataSet = ExecuteQuery($"SELECT * FROM Recipes WHERE RecipeName='{name}';");
            DataRow[] dataRow = dataSet.Tables[0].Select();
            DataRow row = dataRow[0];


            foreach (DataRow recipeVsIngredientRow in recipeVsIngredientDataRow)
            {
                if (row.Field<int>("RecipeID") == recipeVsIngredientRow.Field<int>("RecipeID"))
                {
                    int ingredientID = recipeVsIngredientRow.Field<int>("IngredientID");

                    foreach (Ingredient ingredient in allIngredients)
                    {
                        if (ingredient.ID == ingredientID)
                        {
                            ingredientsInRecipe.Add(ingredient);
                        }
                    }
                }
            }


            Recipe recipe = new Recipe(
            row.Field<string>("RecipeName"),
            ingredientsInRecipe,
            row.Field<int>("RecipeID")
            );

            return recipe;
        }

        public bool NewIngredient(Ingredient ingredient)
        {
            bool succeeded = ExecuteNonQuery(
                $"INSERT INTO Ingredients VALUES ('{ingredient.Name}', '{(int)ingredient.Price}', '{ingredient.Type.ToString()}');"
            );

            return succeeded;
        }

        public bool NewRecipe(Recipe recipe)
        {
            bool secondSucceeded = false;

            bool succeeded = ExecuteNonQuery(
                $"INSERT INTO Recipes VALUES ('{recipe.Name}');"
            );

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
               secondSucceeded = ExecuteNonQuery(
                $"INSERT INTO RecipeVsIngredient VALUES ('{ingredient.ID}', '{recipe.ID}');"    
                );
            };

            if (succeeded)
            {
                if (secondSucceeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //EKSTRA Opgave
        //public bool UpdateIngredient(Ingredient ingredient)
        //{

        //}

        public bool UpdateRecipe(Recipe recipe) // ******** FEJL - Denne skal på en måde løbe ALLE ingredienserne igennem individuelt også! Evt få den til at slette alle rækker der har med denne Recipe at gøre, og så bare lave dem på ny, med de nuværende Ingredienser?
        {
            //RecipeVsIngredient
            DataSet recipeVsIngredientDataSet = ExecuteQuery($"SELECT * FROM RecipeVsIngredient WHERE RecipeID='{recipe.ID}';");
            DataRow[] recipeVsIngredientDataRow = recipeVsIngredientDataSet.Tables[0].Select();

            bool isUpdated = ExecuteNonQuery(
                $"UPDATE Recipes" +
                $"SET RecipeName='{recipe.Name}'" +
                $"WHERE RecipeID='{recipe.ID}';"
            );

            //Lav to foreach løkker her? En der siger for hver ingredient i recipe.Ingredients listen, der skal den tjekke om den findes i DB og ellers Oprette den
            //Og så nummer to som så bagefter tjekker om alle i DB der har dennes RecipeID, hvis de ikke findes i recipe.Ingredients listen, så skal de slettes.

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                bool ingredientExist = ExecuteNonQuery(
                    $"SELECT * FROM RecipeVsIngredient WHERE RecipeID='{recipe.ID}';"
                );

                if (!ingredientExist)
                {
                    ExecuteNonQuery(
                        $"INSERT INTO RecipeVsIngredient VALUES ('{ingredient.ID}', '{recipe.ID}');"
                    );
                }
            }

            //Tjekker om der findes nogle Ingrediensen i denne opskrift, som IKKE skal være der længere. Altså som ikke er i recipe.Ingredients listen længere.
            foreach (DataRow row in recipeVsIngredientDataRow)
            {
                if (!recipe.Ingredients.Any(ingredient => ingredient.ID == row.Field<int>("IngredientID")))
                {
                    ExecuteNonQuery(
                        $"DELETE FROM RecipeVsIngredient WHERE IngredientID='{row.Field<int>("IngredientID")}' AND RecipeID='{recipe.ID}';"
                    );
                }
            }

            return isUpdated; //Skal den tjekke om de andre ting er gjort? De er jo ikke nødvendige for at det var en success. Det er jo kun hvis "der er behov" den gør de ting.
        }

        //EKSTRA Opgave
        //public bool DeleteIngredient(Ingredient ingredient)
        //{

        //}

        public bool DeleteRecipe(Recipe recipe)
        {
            bool secondIsDeleted = false;

            bool isDeleted = ExecuteNonQuery(
                $"DELETE FROM Recipes WHERE RecipeID='{recipe.ID}';"
            );

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                secondIsDeleted = ExecuteNonQuery(
                    $"DELETE FROM RecipeVsIngredient WHERE RecipeID='{recipe.ID}';"
                );
            }

            if (isDeleted)
            {
                if (secondIsDeleted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
