using FluentValidation;

namespace IdentecSolutions.Application.Queries.GetEquipmentById
{
    public sealed class GretEquipmentByIdValidator :AbstractValidator<GetEquipmentByIdRequest>
    {
        public GretEquipmentByIdValidator()
        {
            RuleFor(x => x)
            .Must(x => x != null)
            .WithMessage("Empty Request");

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id parametar is required");
        }
    }
}
