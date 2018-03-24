using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.ExternalImportDto;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportSuppliersCommand : ICommand
    {
        private readonly IUserSession userSession;
        private readonly IOnlineStoreContext context;
        private readonly IValidator validator;

        public ImportSuppliersCommand(IUserSession userSession, IOnlineStoreContext context, IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(IValidator));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(IUserSession));
            this.context = context ?? throw new ArgumentNullException(nameof(IOnlineStoreContext));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                return "Login first!";
            }

            if (this.userSession.HasAdminRights())
            {
                const string FailureMessage = "Import rejected. Input is with invalid format.";
                var importString = File.ReadAllText("../../../Datasets/Suppliers.json");
                var deserializedSuppliers = JsonConvert.DeserializeObject<SuppliersImportDto[]>(importString);
                var importResults = new StringBuilder();

                var validSuppliers = new List<Supplier>();

                foreach (var supplierDto in deserializedSuppliers)
                {
                    if (!this.validator.IsValid(supplierDto))
                    {
                        importResults.AppendLine(FailureMessage);
                        continue;
                    }
                    if (!this.context.Towns.Any(a => a.Name == supplierDto.Town))
                    {
                        var townToAdd = new Town { Name = supplierDto.Town };
                        this.context.Towns.Add(townToAdd);
                        this.context.SaveChanges();
                    }
                    var supplierTown = this.context.Towns.FirstOrDefault(f => f.Name == supplierDto.Town);

                    if (!this.context.Addresses.Any(a => a.AddressText == supplierDto.Address))
                    {
                        var addressToAdd = new Address() { AddressText = supplierDto.Address, Town = supplierTown };
                        this.context.Addresses.Add(addressToAdd);
                        this.context.SaveChanges();
                    }
                    var supplierAddress = this.context.Addresses.FirstOrDefault(f => f.AddressText == supplierDto.Address && f.TownId == supplierTown.Id);

                    var supplierToAdd = new Supplier
                    {
                        Name = supplierDto.Name,
                        Phone = supplierDto.Phone,
                        Address = supplierAddress
                    };
                    validSuppliers.Add(supplierToAdd);
                    importResults.AppendLine($"Supplier {supplierDto.Name} added successfully!");
                }

                validSuppliers.ForEach(s => this.context.Suppliers.Add(s));

                this.context.SaveChanges();
                var result = importResults.ToString().Trim();
                return result;
            }
            else
            {
                return "User must be admin or moderator in order to import data!";
            }
        }
    }
}
