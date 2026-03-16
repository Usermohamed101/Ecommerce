using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.infrastruction
{
    public class Product
    {


            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string ImagePath { get; set; } = string.Empty;
            public int StockQuantity { get; set; }
            public long Version { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
            public ICollection<Review> Reviews { get; set; } = new List<Review>();
        

    }
}
