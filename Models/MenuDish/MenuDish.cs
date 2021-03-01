using System.ComponentModel.DataAnnotations;

namespace MenuDishApp.Models.MenuDish
{
    public class MenuDish
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public bool Family { get; set; }

        public string DishType { get; set; }
    }
}
