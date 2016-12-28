using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using NcCqrsPoc.Domain.ReadModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NcCqrsPoc.Web.Queries.Controllers
{
    // ASP.NET Core - Real-World ASP.NET Core MVC Filtes, lookup reference for async await integration an stuff like that
    // https://msdn.microsoft.com/en-us/magazine/mt767699.aspx
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeesController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        // GET: api/employees/3
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EmployeeRM employee;
            try
            {
                employee = _employeeRepo.GetByID(id);
            }
            catch (ArgumentNullException)
            {
                employee = null;
            }

            //It is possible for GetByID() to return null.
            //If it does, we return HTTP 400 Bad Request
            if (employee == null)
            {
                return NotFound("No Employee with ID " + id.ToString() + " was found."); //404, -> badrequest = 400 as alternativ
            }

            // Otherwise, we return the employee
            return Ok(employee);
        }

        // GET: api/employees
        [HttpGet] // Route("all");
        public IEnumerable<EmployeeRM> Get()
        {
            var employees = _employeeRepo.GetAll();
            return employees;
        }

        // GET: api/employees/all
        [HttpGet("all")] // Route("all");
        public IActionResult GetAll()
        {
            var employees = _employeeRepo.GetAll();
            return Ok(employees);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
