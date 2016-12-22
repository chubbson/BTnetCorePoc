using CQRSlite.Domain;
using NcCqrsPoc.Domain.Events.Subsidiaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.WriteModel.AggrRoot
{
    /// <summary>
    /// Subsidiary Aggregate Root class 
    /// interprete a physical location or 'Filiale'
    /// contains a list registred employees
    /// </summary>
    public class Subsidiary : AggregateRoot
    {
        private int _subsidiaryID;
        private string _streetAddress;
        private string _city;
        private string _postalCode;
        private List<int> _employees;

        // private default constructor
        private Subsidiary() { }

        public Subsidiary(Guid id, int subsidiaryID, string streetAddress, string city, string postalCode)
        {
            Id = id;
            _subsidiaryID = subsidiaryID;
            _streetAddress = streetAddress;
            _city = city;
            _postalCode = postalCode;
            _employees = new List<int>();

            // apply Events
            ApplyChange(new SubsidiaryCreatedEvent(id, subsidiaryID, streetAddress, city, postalCode));
        }
    }
}
