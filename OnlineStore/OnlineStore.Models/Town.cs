using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Town
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } //navprop
    }
}
