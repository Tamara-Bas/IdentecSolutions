using FluentValidation;

namespace IdentecSolutions.Application.Commands.Equipment.DeleteEquipment
{
    public class DeleteEquipmentByIdValidator:AbstractValidator<DeleteEquipmentByIdRequest>
    {
        public DeleteEquipmentByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required")
                .GreaterThan(0)
                .WithMessage("Id must be grater than zero");
        }
    }
}
