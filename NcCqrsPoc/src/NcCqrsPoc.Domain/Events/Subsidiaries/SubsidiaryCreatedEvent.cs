using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Events.Subsidiaries
{
    public class SubsidiaryCreatedEvent : BaseEvent
    {
        public readonly int SubsidiaryID;
        public readonly string StreetAddress;
        public readonly string City;
        public readonly string PostalCode;

        public SubsidiaryCreatedEvent(Guid id, int subsidiaryID, string streetAddress, string city, string state, string postalCode)
        {
            Id = id;
            SubsidiaryID = subsidiaryID;
            StreetAddress = streetAddress;
            City = city;
            PostalCode = postalCode;
        }
    }
}
