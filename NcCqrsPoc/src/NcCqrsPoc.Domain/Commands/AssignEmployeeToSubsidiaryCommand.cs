using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.Commands
{
    public class AssignEmployeeToSubsidiaryCommand : BaseCommand
    {
        public readonly int EmployeeID;
        public readonly int SubsidiaryID;

        public AssignEmployeeToSubsidiaryCommand(Guid id, int subsidiaryID, int employeeID)
        {
            Id = id;
            EmployeeID = employeeID;
            SubsidiaryID = subsidiaryID;
        } 
    }
}
