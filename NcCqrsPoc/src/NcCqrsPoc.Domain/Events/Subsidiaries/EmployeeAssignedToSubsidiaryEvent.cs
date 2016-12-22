using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Events.Subsidiaries
{
    public class EmployeeAssignedToSubsidiaryEvent : BaseEvent
    {
        public readonly int NewSubsidiaryID;
        public readonly int EmployeeID;

        public EmployeeAssignedToSubsidiaryEvent(Guid id, int newSubsidiaryID, int employeeID)
        {
            Id = id;
            NewSubsidiaryID = newSubsidiaryID;
            EmployeeID = employeeID;
        }
    }
}
