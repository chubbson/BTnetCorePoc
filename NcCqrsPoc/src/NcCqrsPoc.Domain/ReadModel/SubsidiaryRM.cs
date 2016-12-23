using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.ReadModel
{
    public class SubsidiaryRM
    {
        public int SubsidiaryID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public List<int> Employees { get; set; }
        public Guid AggregateID { get; set; }

        public SubsidiaryRM()
        {
            Employees = new List<int>();
        }
    }
}
