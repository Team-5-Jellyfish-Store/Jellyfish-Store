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
    public class ImportCouriersCommand : ICommand
    {
        private readonly IUserSession userSession;
        private readonly IOnlineStoreContext context;
        private readonly IValidator validator;

        public ImportCouriersCommand(IUserSession userSession, IOnlineStoreContext context,
            IValidator validator)
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

            if (!this.userSession.HasAdminRights())
            {
                return "User must be admin or moderator in order to import data!";
            }

            var importString = File.ReadAllText("../../../Datasets/Couriers.json");

            var deserializedCouriers = JsonConvert.DeserializeObject<CourierImportDto[]>(importString);
            var importResults = new StringBuilder();

            var validCouriers = new List<Courier>();

            foreach (var courierDto in deserializedCouriers)
            {
                if (!this.validator.IsValid(courierDto))
                {
                    importResults.AppendLine("Import rejected. Input is with invalid format.");
                    continue;
                }

                if (this.context.Couriers.Any(a => a.FirstName == courierDto.FirstName && a.LastName == courierDto.LastName))
                {
                    importResults.AppendLine("Import rejected. Duplicate courier found!");
                    continue;
                }

                if (!this.context.Towns.Any(a => a.Name == courierDto.Town))
                {
                    var townToAdd = new Town() { Name = courierDto.Town };
                    this.context.Towns.Add(townToAdd);
                    this.context.SaveChanges();
                }
                var courierTown = this.context.Towns.FirstOrDefault(f => f.Name == courierDto.Town);

                if (!this.context.Addresses.Any(a => a.AddressText == courierDto.Address))
                {
                    var addressToAdd = new Address() { AddressText = courierDto.Address, TownId = courierTown.Id };
                    this.context.Addresses.Add(addressToAdd);
                    this.context.SaveChanges();
                }
                var courierAddress = this.context.Addresses.FirstOrDefault(f => f.AddressText == courierDto.Address);

                var courierToAdd = new Courier()
                {
                    FirstName = courierDto.FirstName,
                    LastName = courierDto.LastName,
                    Phone = courierDto.Phone,
                    Address = courierAddress
                };
                validCouriers.Add(courierToAdd);
                importResults.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} added successfully!");
            }

            validCouriers.ForEach(c => this.context.Couriers.Add(c));
            this.context.SaveChanges();
            var result = importResults.ToString().Trim();
            return result;
        }
    }
}