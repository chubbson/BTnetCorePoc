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

        /// <summary>
        /// Subsidiary Constructor
        /// Applies SubsidiaryCreatedEvent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subsidiaryID"></param>
        /// <param name="streetAddress"></param>
        /// <param name="city"></param>
        /// <param name="postalCode"></param>
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

        /// <summary>
        /// Add Employee to Subsidiary
        /// Applies EmployeeAssignedToSubsidiaryEvent
        /// </summary>
        /// <param name="employeeID"></param>
        public void AddEmployee(int employeeID)
        {
            _employees.Add(employeeID);
            throw new NotImplementedException("Applies EmployeeAssignedToSubsidiaryEvent");
        }

        /// <summary>
        /// Remove from Subsidiary
        /// Applies EmployeeRemovedFromSubsidiaryEvent
        /// </summary>
        /// <param name="employeeID"></param>
        public void RemoveEmployee(int employeeID)
        {
            _employees.Remove(employeeID);
            throw new NotImplementedException("Applies EmployeeRemovedFromSubsidiaryEvent");
        }
    }
}
