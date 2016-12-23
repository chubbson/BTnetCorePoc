using CQRSlite.Commands;
using CQRSlite.Domain;
using NcCqrsPoc.Domain.Commands;
using NcCqrsPoc.Domain.WriteModel.AggrRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.CommandHandlers
{
    public class SubsidiaryCommandHandler : ICommandHandler<CreateSubsidiaryCommand>, 
                                            ICommandHandler<AssignEmployeeToSubsidiaryCommand>, 
                                            ICommandHandler<RemoveEmployeeFromSubsidiaryCommand>
    {
        private readonly ISession _session;

        public SubsidiaryCommandHandler(ISession session)
        {
            _session = session;
        }
        public void Handle(CreateSubsidiaryCommand command)
        {
            var subsidiary = new Subsidiary(command.Id, command.SubsidiaryID, command.StreetAddress, command.City, command.PostalCode);
            _session.Add(subsidiary);
            _session.Commit();
        }

        public void Handle(AssignEmployeeToSubsidiaryCommand command)
        {
            Subsidiary subsidiary = _session.Get<Subsidiary>(command.Id);
            subsidiary.AddEmployee(command.EmployeeID);
            _session.Commit();
        }

        public void Handle(RemoveEmployeeFromSubsidiaryCommand command)
        {
            Subsidiary subsidiary = _session.Get<Subsidiary>(command.Id);
            subsidiary.RemoveEmployee(command.EmployeeID);
            _session.Commit();
        }
    }
}
