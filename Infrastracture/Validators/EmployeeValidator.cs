using DataModel;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Validators
{
    public class EmployeeValidator: AbstractValidator<Employee>
    {
        private readonly EmployeeDbContext _dbContext;
        public EmployeeValidator(EmployeeDbContext dbContext)
          
        {

         _dbContext = dbContext;

            RuleFor(x => x.FirstName) .NotEmpty().WithMessage("hhhhhhhhh") .NotNull().Length(3, 25);
            RuleFor(x => x.LastName) .NotEmpty() .NotNull().Length(3, 25);
            RuleFor(x => x.DepartmentId).NotEmpty().Must(BeValidDepartmentId).WithMessage("Must Be Valid Department Id");
            RuleFor(x => x.Gender).MinimumLength(4).MaximumLength(6).When(x => x.Gender!= null);
        }

        private bool BeValidDepartmentId(int DepartmentId)
        {
            return _dbContext.Departments.Where(x => x.DepartmentId == DepartmentId).Any();

        }
    }
}
