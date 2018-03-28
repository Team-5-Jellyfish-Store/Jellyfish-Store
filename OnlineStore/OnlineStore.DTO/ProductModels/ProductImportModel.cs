using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.ProductModels.Contracts;

namespace OnlineStore.DTO.ProductModels
{
    public class ProductImportModel : IMapTo<Product>, IHaveCustomMappings, IProductImportModel
    {
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Category name should be at least 3 characters")]
        [MaxLength(30, ErrorMessage = "Category name should be shorter than 30 characters")]
        public string CategoryName { get; set; }

        [Required]
        public string SupplierName { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IProductImportModel, Product>()
                .ForMember(x => x.SellingPrice, cfg => cfg.MapFrom(x => x.PurchasePrice * 1.5m));
        }
    }
}
