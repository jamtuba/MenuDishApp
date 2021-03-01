using HtmlAgilityPack;
using MenuDishApp.Models.MenuDish;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuDishApp.Data.HTMLReader
{
    public class HTMLReader
    {
        static readonly List<MenuDish> MenuList = new List<MenuDish>();
        public static async Task ReadHTML()
        {
            string targetStr = "//*[contains(@class, 'menu-list__title')] | //*[contains(@class, 'menu-list__item')]";

            HtmlWeb web = new HtmlWeb();

            HtmlDocument document = await web.LoadFromWebAsync("http://billundpizza.dk/menu/");

            HtmlNodeCollection targetNodes = document.DocumentNode.SelectNodes(targetStr);

            GetNodes(targetNodes);
        }

        private static void GetNodes(HtmlNodeCollection nodeArray)
        {
            string DishType = "Mad";
            foreach (var node in nodeArray)
            {
                if (node.OriginalName.Contains("h2") && !node.InnerText.Contains("Drikkevarer") && !node.InnerText.Contains("Ekstra tilbehør"))
                {
                    DishType = node.InnerText;
                }

                MenuDish dish = new MenuDish();
                string n = node.InnerText;
                if (node.OriginalName.Contains("h4") && (n != "" && char.IsDigit(n[0])))
                {
                    dish.DishType = DishType;

                    if (n.Contains("."))
                    {
                        dish.Number = n.Substring(0, n.IndexOf("."));
                        dish.Name = n.Substring(n.IndexOf(".") + 1);
                    }
                    else
                    {
                        dish.Number = n.Substring(0, n.IndexOf(" "));
                        dish.Name = n.Substring(n.IndexOf(" "));
                    }
                    if (MenuList.Count != 0)
                    {
                        foreach (var item in MenuList)
                        {
                            if (item.Number == dish.Number)
                                dish.Family = true;
                        }
                    }
                    MenuList.Add(dish);
                }

                if (MenuList.Count != 0)
                {
                    int currentListNumber = MenuList.Count - 1;
                    if (node.OriginalName.Contains("p") && MenuList[currentListNumber].Number != "" && MenuList[currentListNumber].Description is null)
                    {
                        MenuList[currentListNumber].Description = node.InnerText;

                    }
                    if (double.TryParse(node.InnerText.Substring(4), out double price))
                    {
                        if (node.OriginalName.Contains("span") && MenuList[currentListNumber].Number != "")
                        {
                            MenuList[currentListNumber].Price = price;
                        }
                    }
                }
            }
        }

        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MenuDishContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MenuDishContext>>());
            // Look for any movies.
            if (context.MenuDish.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                await ReadHTML();
                foreach (var item in MenuList)
                {
                    context.MenuDish.Add(item);
                    context.SaveChanges();
                }
            }

        }
    }
}