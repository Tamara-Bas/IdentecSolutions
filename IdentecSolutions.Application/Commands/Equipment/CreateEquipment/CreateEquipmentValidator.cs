using FluentValidation;
using IdentecSolutions.Domain.Enums;
using System.Globalization;

namespace IdentecSolutions.Application.Commands.Equipment.CreateEquipment
{
    public class CreateEquipmentValidator :AbstractValidator<CreateEquipmentRequest>
    {
        private const string ExpectedDateFormat = "dd-MM-yyyy"; // Set required format
        public CreateEquipmentValidator()
        {
            RuleFor(x => x.Name)
                //.NotNull()
                .NotEmpty()
                .WithMessage("Name is required")
                            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name cannot be only spaces.")

                .Must(name => name?.Trim().Length > 0)
                .WithMessage("Name cannot be only spaces.")
                .MaximumLength(50)
                .WithMessage("Maximum length is 50");

            RuleFor(x => x.Description)
               .NotNull()
               .NotEmpty()
               .WithMessage("Description is required")
               .MaximumLength(200)
               .WithMessage("Maximum length is 200");

            RuleFor(x => x.SerialNumber)
               .NotNull()
               .NotEmpty()
               .WithMessage("Serial Number is required");


            RuleFor(x => x.Price)
               .GreaterThan(0)
               .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Location)
             .NotNull()
             .NotEmpty()
             .WithMessage("Location is required")
             .MaximumLength(50)
             .WithMessage("Maximum length is 50");

            RuleFor(x => x.EquipmentType)
              .NotNull()
              .NotEmpty()
              .WithMessage("EquipmentType is required")
              .Must(equipmentType => Enum.IsDefined(typeof(EquipmentTypeEnum),(int)equipmentType))
              .When(x=>x.EquipmentType==0 || x.EquipmentType>3)
              .WithMessage("Invalid equipment value. Allowed values: Internal=1,Outdoor=2,Mountain=3.");

            RuleFor(x => x.Status)
           .NotNull()
           .WithMessage("Status is required");


            RuleFor(x => x.WarrantyExpiryDate)
            .Must(date => date != default(DateTime))
            .WithMessage("WarrantyExpiryDate is invalid.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("WarrantyExpiryDate cannot be in the future.")
            .Must(BeInExpectedFormat)
            .WithMessage($"WarrantyExpiryDate must be in the format {ExpectedDateFormat}.")
            .When(e => e.WarrantyExpiryDate.HasValue);


        }
        private bool BeInExpectedFormat(DateTime? date)
        {
            var te = date?.ToString(ExpectedDateFormat, CultureInfo.InvariantCulture);
            return date?.ToString(ExpectedDateFormat, CultureInfo.InvariantCulture) == date?.ToString("dd-MM-yyyy");
        }
    }
}
