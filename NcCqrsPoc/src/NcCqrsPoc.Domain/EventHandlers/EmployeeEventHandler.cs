using CQRSlite.Events;
using NcCqrsPoc.Domain.Events.Employees;
using NcCqrsPoc.Domain.ReadModel;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.EventHandlers
{
    public class EmployeeEventHandler : IEventHandler<EmployeeCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeEventHandler(IMapper mapper, IEmployeeRepo employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo; 
        }

        public void Handle(EmployeeCreatedEvent message)
        {
            /*
            var emloyee = new EmployeeRM() { AggregateID = !!!magicstuff!!! ...,
                                             EmployeeID = message.EmployeeID,
                                             FirstName = message.FirstName,
                                             LastName = message.LastName,
                                             DateOfBirth = message.DateOfBirth,
                                             JobTitle = message.JobTitle,
                                             SubsidiaryID = !!!magicstuff!!!... }
                AggregateId and SubsidiaryId will not be set, should be default Value/null sth like that. 
                it is not explicity used in employeeRepo save command
             this magic stuff should be handelt by IMapper.Map method, 
             IMapper is form AutoMapper
             Dependency Injection which will setup in part4 
             does greatly simplifies event hanlder
             */
             EmployeeRM employee = _mapper.Map<EmployeeRM>(message);
            _employeeRepo.Save(employee);
        }
    }
}
