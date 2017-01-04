using AutoMapper;
using NcCqrsPoc.Domain.Commands;
using NcCqrsPoc.Domain.Events.Subsidiaries;
using NcCqrsPoc.Domain.ReadModel;
using NcCqrsPoc.Web.Commands.Requests.Subsidiaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Web.Commands.AutoMapperConfig
{
    public class SubsidiaryProfile : Profile
    {
        public SubsidiaryProfile()
        {
            CreateMap<CreateSubsidiaryRequest, CreateSubsidiaryCommand>()
                .ConstructUsing(x => new CreateSubsidiaryCommand(Guid.NewGuid(), x.SubsidiaryID, x.StreetAddress, x.City, x.PostalCode));

            CreateMap<SubsidiaryCreatedEvent, SubsidiaryRM>()
                .ForMember(dest => dest.AggregateID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
