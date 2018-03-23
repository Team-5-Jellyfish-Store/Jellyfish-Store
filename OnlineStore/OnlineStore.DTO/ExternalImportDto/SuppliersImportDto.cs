using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO.ExternalImportDto
{
    public class SuppliersImportDto : IMapTo<Supplier>, IHaveCustomMappings
    {
        [Required]
        [MinLength(2, ErrorMessage = "Company name should be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "Company name should be shorter than 20 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 6, ErrorMessage = "Please enter correct Phone")]
        public string Phone { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 4)]
        public string Address { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Town { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SuppliersImportDto, Supplier>()
                .ForPath(x => x.Address.AddressText, cfg => cfg.MapFrom(x => x.Address))
                .ForPath(x => x.Address.Town.Name, cfg => cfg.MapFrom(x => x.Town));
        }
    }
}
