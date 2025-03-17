using FluentValidation;

namespace IdentecSolutions.Application.Commands.Equipment.UpdateEquipment
{
    public class UpdateEquipmentValidator : AbstractValidator<UpdateEquipmentRequest>
    {
        public UpdateEquipmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required")
                .GreaterThan(0)
                .WithMessage("Id must be grater than zero");
        }
    }
}
