using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
// get rid of this static stackExchange.redis eventstore using
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.ReadModel.Repos
{
    /// <summary>
    /// EmployeeRepository inherit form BaseRepository and IEmployeeRepo
    /// BaseRepository needs Stackexchange.Redis as Data Store
    /// TODO: abstract Stackexchange.Redis to connect possible EventStore alternatives 
    /// </summary>
    public class EmployeeRepo : BaseRepository, IEmployeeRepo
    {
        const string c_allCollection = "all";
        const string c_employeeNameSpace = "employee";

        /// <summary>
        /// EmployeeRepo Constructor, using stackexchange.redis IConnectionMultiplexer redisConnection
        /// ToDo: abstract this constructor to assign event store alternatives
        /// </summary>
        /// <param name="redisConnection"></param>
        public EmployeeRepo(IConnectionMultiplexer redisConnection)
            : base(redisConnection, c_employeeNameSpace) { }

        public EmployeeRM GetByID(int employeeID)
        {
            return Get<EmployeeRM>(employeeID);
        }

        public List<EmployeeRM> GetMultiple(List<int> employeeIDs)
        {
            return GetMultiple<EmployeeRM>(employeeIDs);
        }
        
        public IEnumerable<EmployeeRM> GetAll()
        {
            return Get<List<EmployeeRM>>(c_allCollection);
        }
        
        public void Save(EmployeeRM employee)
        {
            Save(employee.EmployeeID, employee);
            MergeIntoAllCollection(employee);
        }

        private void MergeIntoAllCollection(EmployeeRM employee)
        {
            List<EmployeeRM> allEmployees = new List<EmployeeRM>();
            if (Exists(c_allCollection))
            {
                allEmployees = Get<List<EmployeeRM>>(c_allCollection);
            }

            //If the district already exists in the ALL collection, remove that entry
            if (allEmployees.Any(x => x.EmployeeID == employee.EmployeeID))
            {
                allEmployees.Remove(allEmployees.First(x => x.EmployeeID == employee.EmployeeID));
            }

            // Add the modified district to the ALL collection
            allEmployees.Add(employee);

            Save(c_allCollection, allEmployees);
        }
        
        //Exists is implemented by BaseRepository
        //public bool Exists(int id) ...
        
    }
}
