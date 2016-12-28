using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using NcCqrsPoc.Domain.ReadModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NcCqrsPoc.Web.Queries.Controllers
{
    [Route("api/[controller]")]
    public class SubsidiariesController : Controller
    {
        private readonly ISubsidiaryRepo _subsidiaryRepo;

        public SubsidiariesController(ISubsidiaryRepo subsidiaryRepo)
        {
            _subsidiaryRepo = subsidiaryRepo;
        }

        // GET: api/subsidiaries/3
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            SubsidiaryRM subsidiary;
            try
            {
                subsidiary = _subsidiaryRepo.GetByID(id);
            }
            catch (ArgumentNullException)
            {
                subsidiary = null;
            }

            //It is possible for GetByID() to return null.
            //If it does, we return HTTP 400 Bad Request
            if (subsidiary == null)
            {
                return NotFound("No subsidiary with ID " + id.ToString() + " was found."); //404, -> badrequest = 400 as alternativ
            }

            // Otherwise, we return the employee
            return Ok(subsidiary);
        }

        // GET: api/subsidiaries
        [HttpGet] // Route("all");
        public IEnumerable<SubsidiaryRM> Get()
        {
            var subsidiaries = _subsidiaryRepo.GetAll();
            return subsidiaries;
        }

        // GET: api/subsidiaries/all
        [HttpGet("all")] 
        public IActionResult GetAll()
        {
            var employees = _subsidiaryRepo.GetAll();
            return Ok(employees);
        }

        // GET: api/subsidiaries/5/employees
        [HttpGet("{id}/employees")]
        public IActionResult GetEmployees(int id)
        {
            var employees = _subsidiaryRepo.GetEmployees(id);
            return Ok(employees);
        }
    }
}
