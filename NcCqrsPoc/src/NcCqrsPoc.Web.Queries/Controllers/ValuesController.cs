using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;

namespace NcCqrsPoc.Web.Queries.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        
        private readonly IEmployeeRepo _employeeRepo;
        public ValuesController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo; 
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<NcCqrsPoc.Domain.ReadModel.EmployeeRM> Get()
        {
            var allEmployees = _employeeRepo.GetAll();
            return allEmployees;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
