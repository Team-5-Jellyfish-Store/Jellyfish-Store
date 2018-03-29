using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.DTO.CourierModels
{
    public class CourierImportModel : IMapTo<Courier>, IHaveCustomMappings, ICourierImportModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "First name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "First name should be shorter than 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name should be atleast 2 characters")]
        [MaxLength(20, ErrorMessage = "Last name should be shorter than 20 characters")]
        public string LastName { get; set; }

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
            configuration.CreateMap<CourierImportModel, Courier>()
                .ForPath(x => x.Address.AddressText, cfg => cfg.MapFrom(x => x.Address))
                .ForPath(x => x.Address.Town.Name, cfg => cfg.MapFrom(x => x.Town));
        }
    }
}
