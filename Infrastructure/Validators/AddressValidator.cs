using DataModel.DTO;
using FluentValidation;


namespace Infrastructure.Validators
{
  public  class AddressValidator: AbstractValidator<CreateAddressDto>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Zone).NotEmpty().NotNull();
            RuleFor(x => x.Woreda).NotEmpty().NotNull();
            RuleFor(x => x.Region).NotEmpty().NotNull();
        }
    }
}
