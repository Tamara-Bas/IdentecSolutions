using FluentValidation;

namespace IdentecSolutions.Application.Queries.GetEquipmentById
{
    public sealed class GetEquipmentByIdValidator :AbstractValidator<GetEquipmentByIdRequest>
    {
        public GetEquipmentByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Id parametar is required");
        }
    }
}
