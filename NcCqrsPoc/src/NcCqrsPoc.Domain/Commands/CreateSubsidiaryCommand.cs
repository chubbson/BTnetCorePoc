using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Commands
{
    public class CreateSubsidiaryCommand : BaseCommand
    {
        public readonly int SubsidiaryID;
        public readonly string StreetAddress;
        public readonly string City;
        public readonly string PostalCode;

        public CreateSubsidiaryCommand(Guid id, int subsidiaryID, string streetAddress, string city, string postalCode)
        {
            Id = id;
            SubsidiaryID = subsidiaryID;
            StreetAddress = streetAddress;
            City = city;
            PostalCode = postalCode;
        }
    }
}
