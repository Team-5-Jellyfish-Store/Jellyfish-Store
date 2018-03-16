using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class Town
    {
        public Town()
        {
            this.Clients= new HashSet<Client>();
            this.Couriers = new HashSet<Courier>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; } //navprop
        public ICollection<Courier> Couriers { get; set; } //navprop
    }
}
