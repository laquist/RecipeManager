using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DbAccess;

namespace EksamenM2E2017.Opskrifter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields:

        //Connection string:
        //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        private string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();

            // ?????????????? Dette stod der allerede ?????????????

            //List<TestIngredientClass> ingredients = new List<TestIngredientClass>();

            //ingredients.Add(new TestIngredientClass(IngredientType.Grøntsag, "Hvidkål", 15));
            //ingredients.Add(new TestIngredientClass(IngredientType.Fisk, "Torsk", 30));
            //ingredients.Add(new TestIngredientClass(IngredientType.Oksekød, "Oksefars", 35));
            //ingredients.Add(new TestIngredientClass(IngredientType.Grøntsag, "Tomat", 15));
            //ingredients.Add(new TestIngredientClass(IngredientType.Mejeriprodukter, "Ost", 10));
            //ingredients.Add(new TestIngredientClass(IngredientType.Grøntsag, "Chili", 11));
            //ingredients.Add(new TestIngredientClass(IngredientType.Mel, "Hvedemel", 20));
            //ingredients.Add(new TestIngredientClass(IngredientType.Mejeriprodukter, "Gær", 20));
            //ingredients.Add(new TestIngredientClass(IngredientType.Mejeriprodukter, "Mælk", 12));
            //ingredients.Add(new TestIngredientClass(IngredientType.Kolonial, "Sukker", 30));
            
            //DtgAllIngredients.ItemsSource = ingredients;

            //List<TestRecipeClass> recipes = new List<TestRecipeClass>();

            //List<TestIngredientClass> brød = new List<TestIngredientClass>();

            //brød.Add(ingredients[9]);
            //brød.Add(ingredients[8]);
            //brød.Add(ingredients[7]);
            //brød.Add(ingredients[6]);

            //recipes.Add(new TestRecipeClass(brød, "Brød"));

            //ListBoxRecipeList.ItemsSource = recipes;


            DBHandler dbHandler = new DBHandler(conString);
        }
    }
}
