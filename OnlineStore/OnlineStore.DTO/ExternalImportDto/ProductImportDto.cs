using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO.ExternalImportDto
{
    public class ProductImportDto : IMapTo<Product>, IMapFrom<ProductJsonModel>, IHaveCustomMappings
    {
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Category name should be at least 3 characters")]
        [MaxLength(30, ErrorMessage = "Category name should be shorter than 30 characters")]
        public string Category { get; set; }

        [Required]
        public string Supplier { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ProductImportDto, Product>().ForPath(x => x.Category.Name, cfg => cfg.MapFrom(x => x.Category));
            configuration.CreateMap<ProductImportDto, Product>().ForPath(x => x.Supplier.Name, cfg => cfg.MapFrom(x => x.Supplier));
            configuration.CreateMap<ProductImportDto, Product>().ForMember(x => x.SellingPrice,
                cfg => cfg.MapFrom(x => x.PurchasePrice * 1.5m));
        }
    }
}
