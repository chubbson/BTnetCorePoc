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
    /// SubsidiaryRepo inherit form BaseRepository and ISubsidiaryRepo
    /// BaseRepository needs Stackexchange.Redis as Data Store
    /// TODO: abstract Stackexchange.Redis to connect possible EventStore alternatives 
    /// </summary>
    public class SubsidiaryRepo : BaseRepository, ISubsidiaryRepo
    {
        const string c_allCollection = "all";
        const string c_employeeNameSpace = "subsidiary";

        /// <summary>
        /// SubsidiaryRepo Constructor, using stackexchange.redis IConnectionMultiplexer redisConnection
        /// BaseRepository needs Stackexchange.Redis as Data Store
        /// ToDo: abstract this constructor to assign event store alternatives
        /// </summary>
        /// <param name="redisConnection"></param>
        public SubsidiaryRepo(IConnectionMultiplexer redisConnection)
             : base(redisConnection, c_employeeNameSpace) { }

        public SubsidiaryRM GetByID(int subsidiaryID)
        {
            return Get<SubsidiaryRM>(subsidiaryID);
        }

        public List<SubsidiaryRM> GetMultiple(List<int> subsidiaryIDs)
        {
            return GetMultiple(subsidiaryIDs);
        }

        public bool HasEmployee(int subsidiaryID, int employeeID)
        {
            //ToDo: this could possible throw an argument null exception if subsidiary does not exist in store. 
            // Deserialize the SubsidiaryDTO with the key subsidiary:{subsidiaryID}
            var subsidiary = Get<SubsidiaryRM>(subsidiaryID);

            // If that subsidiary has the specified Employee, return true
            return subsidiary.Employees.Contains(employeeID);
        }

        public IEnumerable<SubsidiaryRM> GetAll()
        {
            return Get<List<SubsidiaryRM>>(c_allCollection);
        }

        public IEnumerable<EmployeeRM> GetEmployees(int locationID)
        {
            return Get<List<EmployeeRM>>(locationID.ToString() + ":employees"); 
        }

        public void Save(SubsidiaryRM subsidiary)
        {
            Save(subsidiary.SubsidiaryID, subsidiary);
            MergeIntoAllCollection(subsidiary);
        }

        private void MergeIntoAllCollection(SubsidiaryRM subsidiary)
        {
            List<SubsidiaryRM> allSubsidiaries = new List<SubsidiaryRM>();
            if (Exists(c_allCollection))
            {
                allSubsidiaries = Get<List<SubsidiaryRM>>(c_allCollection);
            }

            //If the district already exists in the ALL collection, remove that entry
            if (allSubsidiaries.Any(x => x.SubsidiaryID == subsidiary.SubsidiaryID))
            {
                allSubsidiaries.Remove(allSubsidiaries.First(x => x.SubsidiaryID == subsidiary.SubsidiaryID));
            }

            //Add the modified district to the ALL collection
            allSubsidiaries.Add(subsidiary);

            Save(c_allCollection, allSubsidiaries);
        }

    }
}
