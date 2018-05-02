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
using Entities;

namespace EksamenM2E2017.Opskrifter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields:
        DBHandler dbHandler;

        //Ny opskrift
        List<Ingredient> listOfAvailableIngredients;
        List<Ingredient> ingredientsInNewRecipe = new List<Ingredient>();

        //Connection string:
        //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        private string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();

            dbHandler = new DBHandler(conString);

            //Opskrifter fanen
            //ShowRecipies();
            UpdateRecipeList();

            //Ingredienser fanen
            DtgIngredients.ItemsSource = dbHandler.GetAllIngredients();
            CbxIngredientType.ItemsSource = Enum.GetValues(typeof(IngredientType)).Cast<IngredientType>();

            //Ny Opskrift fanen
            listOfAvailableIngredients = dbHandler.GetAllIngredients();
            UpdateDataInNewRecipe();


            #region TEMP - StartCode

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
            #endregion
        }

        //public void ShowRecipies()
        //{
        //    List<Recipe> recipeList = dbHandler.GetAllRecipies();
        //    Recipe savedRecipe = null;

        //    if (ListBoxRecipeList.SelectedItem != null)
        //    {
        //        savedRecipe = ListBoxRecipeList.SelectedItem as Recipe;

        //    }
        //    if (ListBoxRecipeList != null)
        //    {
        //        ListBoxRecipeList.ItemsSource = null;
        //        ListBoxRecipeList.Items.Clear();
        //    }

        //    ListBoxRecipeList.ItemsSource = recipeList;

        //    if (savedRecipe != null)
        //    {
        //        ListBoxRecipeList.SelectedItem = recipeList.FirstOrDefault(e => e.ID == savedRecipe.ID);

        //        bool isFound = false;
        //        foreach (Recipe recipe in recipeList)
        //        {
        //            if (recipe.ID == savedRecipe.ID)
        //            {
        //                isFound = true;
        //            }
        //        }

        //        if (isFound)
        //        {
        //            UpdateRecipeInfo(ListBoxRecipeList.SelectedItem as Recipe);
        //        }
        //        else
        //        {
        //            TxtBoxPrice.Text = "";
        //            TxtBoxPersons.Text = "";
        //        }
        //    }
        //}

        public void UpdateRecipeInfo(Recipe recipe)
        {
            TxtBoxPrice.Text = recipe.GetPrice().ToString();
            TxtBoxPersons.Text = recipe.Persons.ToString();
            DtgIngredientsInSelectedRecipe.ItemsSource = recipe.Ingredients;
        }

        public void UpdateRecipeList()
        {
            ListBoxRecipeList.ItemsSource = null;
            ListBoxRecipeList.ItemsSource = dbHandler.GetAllRecipies();
        }

        public void UpdateDataInNewRecipe()
        {
            //Left box
            DtgAllIngredients.ItemsSource = null;
            DtgAllIngredients.ItemsSource = listOfAvailableIngredients;

            //Right box
            DtgItemsInNewRecipe.ItemsSource = null;
            DtgItemsInNewRecipe.ItemsSource = ingredientsInNewRecipe;

            //Her skal den også update Total Pris nede i hjørnet
            decimal totalPrice = 0;
            foreach (Ingredient ingredient in ingredientsInNewRecipe)
            {
                totalPrice = totalPrice + ingredient.Price;
            }
            LblTotalPrice.Content = totalPrice;
        }

        private void ListBoxRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxRecipeList.SelectedItem != null)
            {
                UpdateRecipeInfo(ListBoxRecipeList.SelectedItem as Recipe);
            }
        }

        private void btnNewIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TbxIngredientName.Text))
            {
                if (!String.IsNullOrWhiteSpace(TbxIngredientPrice.Text))
                {
                    if (CbxIngredientType.SelectedItem != null)
                    {
                        IngredientType selectedIngredientType = (IngredientType)CbxIngredientType.SelectedItem;
                        Ingredient newIngredient = new Ingredient(Convert.ToDecimal(TbxIngredientPrice.Text), TbxIngredientName.Text, selectedIngredientType);

                        if (dbHandler.NewIngredient(newIngredient))
                        {
                            MessageBox.Show("Success! Ingrediensen blev tilføjet", "Success!");
                            DtgIngredients.ItemsSource = dbHandler.GetAllIngredients();

                            listOfAvailableIngredients = dbHandler.GetAllIngredients();
                            UpdateDataInNewRecipe();

                            TbxIngredientName.Clear();
                            TbxIngredientPrice.Clear();
                            CbxIngredientType.SelectedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Fejl! - Der skete en fejl. Ingrediensen blev ikke tilføjet", "Fejl!");
                        } 
                    }
                    else
                    {
                        MessageBox.Show("Du skal vælge en type!", "Fejl!");
                    }
                }
                else
                {
                    MessageBox.Show("Du skal angive en pris!", "Fejl!");
                }
            }
            else
            {
                MessageBox.Show("Du skal angive et navn!", "Fejl!");
            }
        }

        private void BtnAddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TxtBoxRecipeName.Text))
            {
                if (!String.IsNullOrWhiteSpace(TxtBoxCountOfPersonsInRecipe.Text))
                {
                    if (ingredientsInNewRecipe.Count != 0)
                    {
                        try
                        {
                            Recipe newRecipe = new Recipe(TxtBoxRecipeName.Text, ingredientsInNewRecipe, int.Parse(TxtBoxCountOfPersonsInRecipe.Text));

                            dbHandler.NewRecipe(newRecipe);

                            MessageBox.Show("Opskriften blev tilføjet!", "Success!");
                            TxtBoxRecipeName.Clear();
                            ingredientsInNewRecipe.Clear();
                            TxtBoxCountOfPersonsInRecipe.Clear();

                            listOfAvailableIngredients = dbHandler.GetAllIngredients();
                            UpdateDataInNewRecipe();
                            UpdateRecipeList();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Der opstod en fejl! \n{ex.Message}", "Fejl");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Du kan ikke oprette en opskrift uden nogle ingredienser!", "Fejl!");
                    }
                }
                else
                {
                    MessageBox.Show("Du skal angive hvor mange personer opskriften er til!", "Fejl!");
                }
            }
            else
            {
                MessageBox.Show("Du skal give opskriften et navn!", "Fejl!");
            }


            //Update Opskrifter fanens data med den nye opskrift
            //Clear alle felter i Ny opskrift fanen

            //Tjek om input er null osv
        }

        private void BtnMoveItemRight_Click(object sender, RoutedEventArgs e)
        {
            if (DtgAllIngredients.SelectedItem != null)
            {
                ingredientsInNewRecipe.Add(DtgAllIngredients.SelectedItem as Ingredient);
                listOfAvailableIngredients.Remove(DtgAllIngredients.SelectedItem as Ingredient);

                UpdateDataInNewRecipe();
            }
            else
            {
                MessageBox.Show("Du skal først vælge den ingrediens som du vil tilføje!", "Fejl!");
            }
        }

        private void BtnMoveItemLeft_Click(object sender, RoutedEventArgs e)
        {
            if (DtgItemsInNewRecipe.SelectedItem != null)
            {
                listOfAvailableIngredients.Add(DtgItemsInNewRecipe.SelectedItem as Ingredient);
                ingredientsInNewRecipe.Remove(DtgItemsInNewRecipe.SelectedItem as Ingredient);

                UpdateDataInNewRecipe();
            }
            else
            {
                MessageBox.Show("Du skal først vælge den ingrediens som du vil fjerne!", "Fejl!");
            }
        }

        private void BtnWikipediaSummary_Click(object sender, RoutedEventArgs e)
        {
            if (DtgIngredients.SelectedItem != null)
            {
                Ingredient selectedIngredient = DtgIngredients.SelectedItem as Ingredient;

                try
                {
                    WikipediaInfo wikipediaInfo = new WikipediaInfo(selectedIngredient.Name);

                    wikipediaInfo.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Der skete en fejl. Måske findes siden ikke på Wikipedia", "Fejl!");
                }
            }
            else
            {
                MessageBox.Show("Du skal først vælge den ingrediens som du ønsker et summary af!", "Fejl!");
            }
        }
    }
}
