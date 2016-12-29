using FluentValidation;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Web.Commands.Requests.Subsidiaries
{
    public class CreateSubsidiaryRequest
    {
        public int SubsidaryID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class CreateSubsidiaryRequestValidator : AbstractValidator<CreateSubsidiaryRequest>
    {
        public CreateSubsidiaryRequestValidator(ISubsidiaryRepo subsidiaryRepo)
        {
            RuleFor(x => x.SubsidaryID).Must(x => !subsidiaryRepo.Exists(x)).WithMessage("A Subsidiary with this ID already exists.");
            RuleFor(x => x.StreetAddress).NotNull().NotEmpty().WithMessage("The Street Address cannot be null");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("The City cannot be null");
            RuleFor(x => x.PostalCode).NotNull().NotEmpty().WithMessage("The Postal Code cannot be null");
        }
    }
}
