using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Events.Subsidiaries
{
    public class EmployeeRemovedFromSubsidiaryEvent : BaseEvent
    {
        public readonly int OldSubsidiaryID;
        public readonly int EmployeeId;

        public EmployeeRemovedFromSubsidiaryEvent(Guid id, int oldSubsidiaryID, int employeeId)
        {
            Id = id;
            OldSubsidiaryID = oldSubsidiaryID;
            EmployeeId = employeeId; 
        }
    }
}
