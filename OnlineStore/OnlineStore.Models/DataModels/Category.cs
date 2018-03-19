using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
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
        [MaxLength(30, ErrorMessage = "Category name should be shorter than 30 characters")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } //navprop
    }
}
