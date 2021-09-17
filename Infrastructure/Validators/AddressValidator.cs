using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using DataModel.Entity;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class AddressValidator: AbstractValidator<Address>
    {
        private readonly EmployeeDbContext _dbcontext;
        public AddressValidator(EmployeeDbContext dbContext)
        {
            _dbcontext = dbContext;
            RuleFor(x => x.Region)
                .NotEmpty().NotNull()
                .WithMessage("Region cannot be null")
                .Length(3, 25);

            RuleFor(x => x.Woreda)
                .NotEmpty().NotNull()
                .Length(6, 15);

            RuleFor(x => x.AddressId).NotEmpty()
                .Must(BeValidAddress)
                .WithMessage("AddressId must be valid");

            RuleFor(x => x.Zone)
                .MinimumLength(4)
                .MaximumLength(6)
                .When(x => x.Zone != null);
        }
        private bool BeValidAddress(int AddressId)
        {
            return _dbcontext.Departments.Where(x => x.DepartmentId == AddressId).Any();
        }

    }

    }

