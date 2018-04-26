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
    public class IngredientTests
    {
        [TestMethod()]
        public void IngredientTest()
        {
            //Arrange
            Ingredient testIngredient = new Ingredient(10, "Rodfrugt", IngredientType.RootVegetables);

            //Act


            //Assert
            Assert.AreEqual(testIngredient.Name, "Rodfrugt");
        }
    }
}