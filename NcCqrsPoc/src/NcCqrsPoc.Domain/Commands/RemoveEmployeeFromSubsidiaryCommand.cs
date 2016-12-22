using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Commands
{
    public class RemoveEmployeeFromSubsidiaryCommand : BaseCommand
    {
        public readonly int EmployeeID;
        public readonly int LocationId;

        public RemoveEmployeeFromSubsidiaryCommand(Guid id, int subsidiaryID, int employeeID)
        {
            Id = id;
            EmployeeID = employeeID;
            SubsidiaryID = subsidiaryID;
        }
    }
}
