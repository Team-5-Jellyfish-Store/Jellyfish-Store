using System;
using System.Collections.Generic;

namespace OnlineStore.DTO
{
    public class OrderMakeModel
    {
        public IDictionary<string, int> ProductNameAndCounts { get; set; }
        public string Comment { get; set; }
        public DateTime OrderedOn { get; set; }
        public string Username { get; set; }
    }
}