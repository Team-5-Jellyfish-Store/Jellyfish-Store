using System;
using System.Collections.Generic;

namespace OnlineStore.DTO.OrderModels
{
    public class OrderMakeModel
    {
        private string comment;

        public OrderMakeModel()
        {
            this.ProductNameAndCounts = new Dictionary<string, int>();
        }

        public IDictionary<string, int> ProductNameAndCounts { get; set; }

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = null;
                }

                this.comment = value;
            }
        }

        public DateTime OrderedOn { get; set; }

        public string Username { get; set; }
    }
}