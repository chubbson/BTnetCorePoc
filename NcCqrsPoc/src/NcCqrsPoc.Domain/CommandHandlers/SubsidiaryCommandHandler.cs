﻿using CQRSlite.Commands;
using CQRSlite.Domain;
using NcCqrsPoc.Domain.Commands;
using NcCqrsPoc.Domain.WriteModel.AggrRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.CommandHandlers
{
    public class SubsidiaryCommandHandler : ICommandHandler<CreateSubsidiaryCommand>
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
    }
}