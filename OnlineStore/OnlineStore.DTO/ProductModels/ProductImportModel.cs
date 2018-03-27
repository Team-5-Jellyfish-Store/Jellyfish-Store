using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OnlineStore.DTO.MappingContracts;
using OnlineStore.Models.DataModels;
using System;

namespace OnlineStore.DTO.ProductModels
{
    public class ProductImportModel : IMapTo<Product>, IHaveCustomMappings
    {
        private string name;
        private decimal purchasePrice;
        private int quantity;
        private string categoryName;
        private string supplierName;

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Product name is required!");
                }

                if (value.Length < 4 || value.Length > 30)
                {
                    throw new ArgumentException("Product name should be at least 4 characters and shorter than 30 characters");
                }

                this.name = value;
            }
        }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal PurchasePrice
        {
            get
            {
                return this.purchasePrice;
            }
            set
            {
                if (value < 0m || value > decimal.MaxValue)
                {
                    throw new ArgumentException("Product's purchase price cannot be negative!");
                }

                this.purchasePrice = value;
            }
        }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (value < 1 || value > int.MaxValue)
                {
                    throw new ArgumentException("Product's quantity cannot be negative!");
                }

                this.quantity = value;
            }
        }

        [Required]
        [MinLength(3, ErrorMessage = "Category name should be at least 3 characters")]
        [MaxLength(30, ErrorMessage = "Category name should be shorter than 30 characters")]
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Category is required!");
                }

                if (value.Length < 3 || value.Length > 30)
                {
                    throw new ArgumentException("Category name should be at least 3 characters and shorter than 30 characters");
                }

                this.categoryName = value;
            }
        }

        [Required]
        public string SupplierName
        {
            get
            {
                return this.supplierName;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Supplier is required!");
                }

                if (value.Length < 2 || value.Length > 20)
                {
                    throw new ArgumentException("Supplier name should be at least 2 characters and shorter than 20 characters");
                }

                this.supplierName = value;
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ProductImportModel, Product>()
                .ForMember(x => x.SellingPrice, cfg => cfg.MapFrom(x => x.PurchasePrice * 1.5m));
        }
    }
}
