using MenuDishApp.Models.Ingredients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MenuDishApp.Data.HTMLReader
{
    public class IngredientsMaker
    {
        static readonly List<Ingredients> IngredientList = new List<Ingredients>();
        public static void IngredientMaker(IServiceProvider serviceProvider)
        {
            using var context = new IngredientContext(
               serviceProvider.GetRequiredService<DbContextOptions<IngredientContext>>());

            if (context.Ingredients.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                GetIngredients();
                foreach (Ingredients ingredient in IngredientList)
                {
                    context.Ingredients.Add(ingredient);
                    context.SaveChanges();
                }
            }
        }
        public static void GetIngredients()
        {
            var logFile = File.ReadAllLines(@"C:\Users\Bruger\Dropbox\Uddannelse\Avanceret Programmering\Mine koder\MenuDishApp\SavedLists.txt");
            var IngredientsList = new List<string>(logFile);
            foreach (string item in IngredientsList)
            {
                Ingredients Ingredient = new Ingredients
                {
                    Ingredient = item.Replace(",", "")
                };

                IngredientList.Add(Ingredient);
            }
        }
    }
}
