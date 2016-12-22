using CQRSlite.Commands;
using NcCqrsPoc.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Domain.CommandHandlers
{
    public class EmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
    {
        public void Handle(CreateEmployeeCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
