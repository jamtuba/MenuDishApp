using MenuDishApp.Models.MenuDish;
using Microsoft.EntityFrameworkCore;

namespace MenuDishApp.Data
{
    public class MenuDishContext : DbContext
    {
        public MenuDishContext(DbContextOptions<MenuDishContext> options) : base(options)
        {
        }

        public DbSet<MenuDish> MenuDish { get; set; }
    }
}
