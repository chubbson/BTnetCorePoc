using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CQRSlite.Commands;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using NcCqrsPoc.Web.Commands.Requests.Subsidiaries;
using NcCqrsPoc.Domain.Commands;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NcCqrsPoc.Web.Commands.Controllers
{
    [Route("api/[controller]")]
    public class SubsidiaryController : Controller
    {
        private IMapper _mapper;
        private ICommandSender _commandSender;
        private ISubsidiaryRepo _subsidiaryRepo;
        private IEmployeeRepo _employeeRepo;

        public SubsidiaryController(ICommandSender commandSender, IMapper mapper, ISubsidiaryRepo subsidiary, IEmployeeRepo employee)
        {
            _commandSender = commandSender;
            _mapper = mapper;
            _subsidiaryRepo = subsidiary;
            _employeeRepo = employee;
        }

        // POST: api/subsidiary/create
        [HttpPost("create")]
        [NcCqrsPoc.Web.Commands.Filters.BadRequestActionFilter]
        public IActionResult Create([FromBody]CreateSubsidiaryRequest request)
        {
            var command = _mapper.Map<CreateSubsidiaryCommand>(request);
            _commandSender.Send(command);
            return Ok();
        }

        [HttpPost("assignemployee")]
        public IActionResult AssignEmployee(AssignEmployeeToSubsidiaryRequest request)
        {
            var employee = _employeeRepo.GetByID(request.EmployeeID);
            if (employee.SubsidiaryID != 0)
            {
                var oldSubsidiaryAggregateId = _subsidiaryRepo.GetByID(employee.SubsidiaryID).AggregateID;

                RemoveEmployeeFromSubsidiaryCommand removeCommand = new RemoveEmployeeFromSubsidiaryCommand(oldSubsidiaryAggregateId, request.SubsidiaryID, employee.EmployeeID);
                _commandSender.Send(removeCommand);
            }

            var locationAggregateID = _subsidiaryRepo.GetByID(request.SubsidiaryID).AggregateID;
            var assignCommand = new AssignEmployeeToSubsidiaryCommand(locationAggregateID, request.SubsidiaryID, request.EmployeeID);
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
