using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Events.Subsidiaries
{
    public class EmployeeRemovedFromSubsidieryEvent : BaseEvent
    {
        public readonly int OldSubsidiaryID;
        public readonly int EmployeeId;

        public EmployeeRemovedFromSubsidieryEvent(Guid id, int oldSubsidiaryID, int employeeId)
        {
            Id = id;
            OldSubsidiaryID = oldSubsidiaryID;
            EmployeeId = employeeId; 
        }
    }
}
