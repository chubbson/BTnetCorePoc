using FluentValidation;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Web.Commands.Requests.Subsidiaries
{
    public class AssignEmployeeToSubsidiaryRequest
    {
        public int SubsidiaryID { get; set; }
        public int EmployeeID { get; set; }
    }

    public class AssignEmployeeToSubsidiaryRequestValidator : AbstractValidator<AssignEmployeeToSubsidiaryRequest>
    {
        public AssignEmployeeToSubsidiaryRequestValidator(IEmployeeRepo employeeRepo, ISubsidiaryRepo subsidiaryRepo)
        {
            RuleFor(x => x.SubsidiaryID).Must(x => subsidiaryRepo.Exists(x)).WithMessage("No Subsidiary with this ID exists.");
            RuleFor(x => x.EmployeeID).Must(x => employeeRepo.Exists(x)).WithMessage("No Employee with this ID exists.");
            RuleFor(x => new { x.SubsidiaryID, x.EmployeeID }).Must(x => !subsidiaryRepo.HasEmployee(x.SubsidiaryID, x.EmployeeID));
        }
    }
}
