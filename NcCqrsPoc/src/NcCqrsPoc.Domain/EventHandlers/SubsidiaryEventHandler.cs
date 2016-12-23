using CQRSlite.Events;
using NcCqrsPoc.Domain.Events.Subsidiaries;
using NcCqrsPoc.Domain.ReadModel;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.EventHandlers
{
    public class SubsidiaryEventHandler : IEventHandler<SubsidiaryCreatedEvent>, 
                                          IEventHandler<EmployeeAssignedToSubsidiaryEvent>, 
                                          IEventHandler<EmployeeRemovedFromSubsidiaryEvent>
    {
        private readonly IMapper _mapper;
        private readonly ISubsidiaryRepo _subsidiaryRepo;
        private readonly IEmployeeRepo _employeeRepo;
        public SubsidiaryEventHandler(IMapper mapper, ISubsidiaryRepo subsidiaryRepo, IEmployeeRepo employeeRepo)
        {
            _mapper = mapper;
            _subsidiaryRepo = subsidiaryRepo;
            _employeeRepo = employeeRepo;
        }

        public void Handle(SubsidiaryCreatedEvent message)
        {
            // Create a new SubsidiaryDTO object form SubsidiaryCreatedEvent
            SubsidiaryRM subsidiary = _mapper.Map<SubsidiaryRM>(message);
            _subsidiaryRepo.Save(subsidiary);
        }

        public void Handle(EmployeeAssignedToSubsidiaryEvent message)
        {
            var subsidiary = _subsidiaryRepo.GetByID(message.NewSubsidiaryID);
            subsidiary.Employees.Add(message.EmployeeID);
            _subsidiaryRepo.Save(subsidiary);

            //Find the employee which was assigned to this subsidiary
            var employee = _employeeRepo.GetByID(message.EmployeeID);
            employee.SubsidiaryID = message.NewSubsidiaryID;
            _employeeRepo.Save(employee);
        }

        public void Handle(EmployeeRemovedFromSubsidiaryEvent message)
        {
            var subsidiary = _subsidiaryRepo.GetByID(message.OldSubsidiaryID);
            subsidiary.Employees.Remove(message.EmployeeId);
            _subsidiaryRepo.Save(subsidiary);
        }
    }
}
