using System;

namespace OnlineStore.DTO
{
    public class OrderModel
    {
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public string Comment { get; set; }
        public DateTime OrderedOn { get; set; }
        public string Username { get; set; }
    }
}
