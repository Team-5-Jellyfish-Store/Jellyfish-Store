using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "First name should be shorter than 20 characters")]
        public string Firstname { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "Last name should be shorter than 20 characters")]
        public string Lastname { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 6, ErrorMessage = "Please enter correct Phone")]
        public string Phone { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<Product> Products { get; set; } //navprop
    }
}
