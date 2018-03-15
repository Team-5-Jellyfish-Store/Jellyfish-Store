using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; } //navprop
        public ICollection<Supplier> Suppliers { get; set; } //navprop
    }
}
