using System.ComponentModel.DataAnnotations;

namespace Paratha.Web.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 100)]
        public double Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
