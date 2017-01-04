using AutoMapper;
using NcCqrsPoc.Domain.Commands;
using NcCqrsPoc.Domain.Events.Employees;
using NcCqrsPoc.Domain.ReadModel;
using NcCqrsPoc.Web.Commands.Requests.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Web.Commands.AutoMapperConfig
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeRequest, CreateEmployeeCommand>()
                .ConstructUsing(x => new CreateEmployeeCommand(Guid.NewGuid(), x.EmployeeID, x.FirstName, x.LastName, x.DateOfBirth, x.JobTitle));

            CreateMap<EmployeeCreatedEvent, EmployeeRM>()
                .ForMember(dest => dest.AggregateID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
