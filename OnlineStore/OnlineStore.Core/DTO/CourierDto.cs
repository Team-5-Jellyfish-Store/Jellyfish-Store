using System.ComponentModel.DataAnnotations;
using OnlineStore.Data.Migrations;
using OnlineStore.Models;

namespace OnlineStore.Core.DTO
{
    public class CourierDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "First name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "First name should be shorter than 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "Last name should be shorter than 20 characters")]
        public string LastName { get; set; }

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
