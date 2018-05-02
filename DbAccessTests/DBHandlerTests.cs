using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DbAccess.Tests
{
    [TestClass()]
    public class DBHandlerTests
    {
        private static string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        DBHandler dbHandler = new DBHandler(conString);

        //Arrange


        //Act


        //Assert


        [TestMethod()]
        public void GetAllIngredientsTest()
        {
            //Arrange
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DBHandler handler = new DBHandler(conString);

            //Act
            List<Ingredient> ingredients = handler.GetAllIngredients();

            //Assert

        }

        [TestMethod()]
        public void DBHandlerTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetAllIngredientsTest1()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetAllRecipiesTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetIngredientByNameTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetRecipeByNameTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void NewIngredientTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void NewRecipeTest()
        {
            //Arrange
            List<Ingredient> recipeIngredients = dbHandler.GetAllIngredients();
            Recipe recipe = new Recipe("TestRecipe48716622", recipeIngredients, 4);

            //Act
            bool succeeded = dbHandler.NewRecipe(recipe);

            //Assert
            Assert.AreEqual(succeeded, true);
        }

        [TestMethod()]
        public void UpdateRecipeTest()
        {
            //Arrange
            List<Ingredient> recipeIngredients = dbHandler.GetAllIngredients();
            Recipe recipe = new Recipe("TestRecipe325254652", recipeIngredients, 4);
            dbHandler.NewRecipe(recipe);

            Recipe recipeWithID = dbHandler.GetRecipeByName(recipe.Name);

            recipeWithID.Persons = 8;

            //Act
            dbHandler.UpdateRecipe(recipeWithID);

            Recipe updatedRecipe = dbHandler.GetRecipeByName(recipeWithID.Name);

            //Assert
            Assert.AreEqual(updatedRecipe.Persons, 8);
        }

        [TestMethod()]
        public void DeleteRecipeTest()
        {
            //Arrange
            List<Ingredient> recipeIngredients = dbHandler.GetAllIngredients();
            Recipe recipe = new Recipe("TestRecipe6585865", recipeIngredients, 4);
            dbHandler.NewRecipe(recipe);

            recipe = dbHandler.GetRecipeByName(recipe.Name);
            //Act
            bool isDeleted = dbHandler.DeleteRecipe(recipe.ID);

            //Assert
            Assert.AreEqual(isDeleted, true);
        }
    }
}