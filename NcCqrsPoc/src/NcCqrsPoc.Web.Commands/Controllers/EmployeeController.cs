using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CQRSlite.Commands;
using AutoMapper;
using NcCqrsPoc.Web.Commands.Requests.Employees;
using NcCqrsPoc.Domain.Commands;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NcCqrsPoc.Web.Commands.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private IMapper _mapper;
        private ICommandSender _commandSender;
        private ISubsidiaryRepo _subsidiaryRepo;

        public EmployeeController(ICommandSender commandSender, IMapper mapper, ISubsidiaryRepo subsidiaryRepo)
        {
            _commandSender = commandSender;
            _mapper = mapper;
            _subsidiaryRepo = subsidiaryRepo;
        }

        // POST: api/employee/create
        [HttpPost("create")]
        //[NcCqrsPoc.Web.Commands.Filters.BadRequestActionFilter]
        public IActionResult Create([FromBody]CreateEmployeeRequest request)
        {
            var command = _mapper.Map<CreateEmployeeCommand>(request);
            _commandSender.Send(command);

            var subsidiaryAggregateID = _subsidiaryRepo.GetByID(request.SubsidiaryID).AggregateID;

            var assignCommand = new AssignEmployeeToSubsidiaryCommand(subsidiaryAggregateID, request.SubsidiaryID, request.EmployeeID);
            _commandSender.Send(assignCommand);
            return Ok();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
