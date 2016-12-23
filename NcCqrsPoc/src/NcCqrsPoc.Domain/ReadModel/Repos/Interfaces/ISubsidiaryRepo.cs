using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.ReadModel.Repos.Interfaces
{
    public interface ISubsidiaryRepo : IBaseRepo<SubsidiaryRM>
    {
        IEnumerable<SubsidiaryRM> GetAll();
        IEnumerable<EmployeeRM> GetEmployees(int subsidiaryID);
        bool HasEmployee(int locationID, int employeeID);
    }
}
