using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Category name should be atleast 3 characters")]
        [MaxLength(15, ErrorMessage = "Category name should be shorter than 15 characters")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } //navprop
    }
}
