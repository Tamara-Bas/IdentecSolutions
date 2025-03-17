﻿using FluentValidation;
using IdentecSolutions.Application.Queries.GetAllEquipment;

namespace IdentecSolutions.Application.Queries.GetAllEquipmentByStatus
{
    public class GetAllEquipmentByStatusValidator : AbstractValidator<GetAllEquipmentByStatusRequest>
    {
        public GetAllEquipmentByStatusValidator()
        {
            RuleFor(x => x.Status)
                .NotNull()
                .NotEmpty()
                .WithMessage("Status parametar is required");
        }
    }
}
