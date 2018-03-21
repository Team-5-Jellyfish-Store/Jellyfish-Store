using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTO
{
    public class ProductImportDto
    {
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Category name should be atleast 3 characters")]
        [MaxLength(30, ErrorMessage = "Category name should be shorter than 30 characters")]
        public string Category { get; set; }

        [Required]
        public string Supplier { get; set; }
    }
}
