using MenuDishApp.Data;
using MenuDishApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MenuDishApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IngredientContext _context;
        private readonly MenuDishContext _contextMenu;

        public HomeController(ILogger<HomeController> logger, IngredientContext context, MenuDishContext contextMenu)
        {
            _logger = logger;
            _context = context;
            _contextMenu = contextMenu;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingredients.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Test()
        {
            return View();
        }



        public async Task<IActionResult> Result(string[] checkbox)
        {

            var ingredients = from i in _contextMenu.MenuDish
                              select i;
            bool excludeInclude = (checkbox.Any() && checkbox[^1] == "on") ? true : false;
            foreach (var item in checkbox)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    if (excludeInclude)
                        ingredients = ingredients.Where(s => s.Description.Contains(item));
                    else
                        ingredients = ingredients.Where(s => !s.Description.Contains(item));
                }
            }

            ingredients = ingredients.OrderBy(i => i.Description);
            //ingredients = from i in ingredients
            //              orderby i.DishType
            //              select i;


            return View(await ingredients.ToListAsync());
        }
    }
}
