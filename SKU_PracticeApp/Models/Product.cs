using System.ComponentModel.DataAnnotations;

namespace SKU_PracticeApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public double Discount { get; set; }
    }
}
