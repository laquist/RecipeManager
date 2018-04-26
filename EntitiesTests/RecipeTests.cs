using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Tests
{
    [TestClass()]
    public class RecipeTests
    {
        [TestMethod()]
        public void GetPriceTest()
        {
            //Arrange
            Ingredient testIngredientEt = new Ingredient(10, "Salt", IngredientType.Conserves);
            Ingredient testIngredientTo = new Ingredient(10, "Peber", IngredientType.Conserves);

            List<Ingredient> testList = new List<Ingredient> {testIngredientEt, testIngredientTo};
            decimal testPrice = testIngredientEt.Price + testIngredientTo.Price;
            
            Recipe testRecipe = new Recipe("Krydderi-ret", testList);

            //Act
            decimal getPrice = testRecipe.GetPrice();

            //Assert
            Assert.AreEqual(getPrice, testPrice);
        }
    }
}