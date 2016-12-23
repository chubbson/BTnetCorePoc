using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.ReadModel.Repos.Interfaces
{
    public interface IEmployeeRepo : IBaseRepo<EmployeeRM>
    {
        IEnumerable<EmployeeRM> GetAll();
    }


}
