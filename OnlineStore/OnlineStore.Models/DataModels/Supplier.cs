using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models.DataModels
{
    public class Supplier
    {
        private ICollection<Product> products;
        public Supplier()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Company name should be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "Company name should be shorter than 20 characters")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 6, ErrorMessage = "Please enter correct Phone")]
        public string Phone { get; set; }

        public int AddressId { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
