using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models.DataModels
{
    public class Town
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } //navprop
    }
}
