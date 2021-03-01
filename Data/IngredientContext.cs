using MenuDishApp.Models.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace MenuDishApp.Data
{
    public class IngredientContext : DbContext
    {
        public IngredientContext(DbContextOptions<IngredientContext> options) : base(options)
        {
        }

        public DbSet<Ingredients> Ingredients { get; set; }

    }
}
