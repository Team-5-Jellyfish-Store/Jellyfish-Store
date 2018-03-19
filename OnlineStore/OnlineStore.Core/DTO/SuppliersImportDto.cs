using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Core.DTO
{
    public class SuppliersImportDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Company name should be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "Company name should be shorter than 20 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 6, ErrorMessage = "Please enter correct Phone")]
        public string Phone { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 4)]
        public string Address { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Town { get; set; }
    }
}
