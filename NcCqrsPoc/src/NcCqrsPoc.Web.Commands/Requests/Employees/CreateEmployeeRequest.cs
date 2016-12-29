﻿using FluentValidation;
using NcCqrsPoc.Domain.ReadModel.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcCqrsPoc.Web.Commands.Requests.Employees
{
    public class CreateEmployeeRequest
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public int SubsidiaryID { get; set; }
    }

    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator(IEmployeeRepo employeeRepo, ISubsidiaryRepo subsidiaryRepo)
        {
            RuleFor(x => x.EmployeeID).Must(x => !employeeRepo.Exists(x)).WithMessage("An Employee with this ID already exists.");
            RuleFor(x => x.SubsidiaryID).Must(x => subsidiaryRepo.Exists(x)).WithMessage("No Subsidiary with this ID exists.");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("The First Name cannot be blank.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("The Last Name cannot be blank.");
            RuleFor(x => x.JobTitle).NotNull().NotEmpty().WithMessage("The Job Title cannot be blank.");
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today.AddYears(-16)).WithMessage("Employees must be 16 years old or older.");
        }
    }
}
