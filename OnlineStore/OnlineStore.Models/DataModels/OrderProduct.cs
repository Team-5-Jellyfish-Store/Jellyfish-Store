using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.DataModels
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        [Required]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        [Required]
        public virtual Product Product { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductCount { get; set; }
    }
}
