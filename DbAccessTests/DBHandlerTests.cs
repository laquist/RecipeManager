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
        [TestMethod()]
        public void GetAllIngredientsTest()
        {
            //Arrange
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DBHandler handler = new DBHandler(conString);

            //Act
            List<Ingredient> ingredients = handler.GetAllIngredients();

            //Assert
            Assert.
        }
    }
}