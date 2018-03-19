using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.DTO;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportCouriersCommand : ICommand
    {
        private readonly IUserSessionService sessionService;
        private readonly IOnlineStoreContext context;

        public ImportCouriersCommand(IUserSessionService sessionService, IOnlineStoreContext context)
        {

            this.sessionService = sessionService;
            this.context = context;

        }

        public string ExecuteThisCommand()
        {
            //if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            //{
            const string FailureMessage = "Courier input is invalid";
            var importString = File.ReadAllText("../../../Datasets/Couriers.json");
            var deserializedCouriers = JsonConvert.DeserializeObject<CourierDto[]>(importString);
            var sb = new StringBuilder();

            var validCouriers = new List<Courier>();

            foreach (var courierDto in deserializedCouriers)
            {
                if (!IsValid(courierDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!this.context.Towns.Any(a => a.Name == courierDto.Town))
                {
                    var townToAdd = new Town() { Name = courierDto.Town };
                    context.Towns.Add(townToAdd);
                    context.SaveChanges();
                }
                var courierTown = this.context.Towns.FirstOrDefault(f => f.Name == courierDto.Town);

                if (!this.context.Addresses.Any(a => a.AddressText == courierDto.Address))
                {
                    var addressToAdd = new Address() { AddressText = courierDto.Address, TownId = courierTown.Id };
                    context.Addresses.Add(addressToAdd);
                    context.SaveChanges();
                }
                var courierAddress = this.context.Addresses.FirstOrDefault(f => f.AddressText == courierDto.Address);

                var courierToAdd = new Courier()
                {
                    FirstName = courierDto.FirstName,
                    LastName = courierDto.LastName,
                    Phone = courierDto.Phone,
                    AddressId = courierAddress.Id
                };
                validCouriers.Add(courierToAdd);
                sb.AppendLine($"Courier {courierDto.FirstName} {courierDto.LastName} added successfully!");
            }

            validCouriers.ForEach(s => this.context.Couriers.Add(s));
            this.context.SaveChanges();
            var result = sb.ToString();
            return result;
            // }

            return "User must be admin or moderator in order to import data!";
        }

        private bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj); // System.Components.Data.Annotations
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }
    }
}
