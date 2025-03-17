using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.EF.UnitOfWork;

namespace IdentecSolutions.Application.Commands.Equipment.CreateEquipment
{
    public class CreateEquipmentHandler : ICommandHandler<CreateEquipmentRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;

        public CreateEquipmentHandler(IUnitOfWork unitOfWork, IEquipmentServiceRepository equipmentServiceRepository)
        {
            _unitOfWork = unitOfWork;
            _equipmentServiceRepository = equipmentServiceRepository;
        }

        public async Task Handle(CreateEquipmentRequest request, CancellationToken cancellationToken)
        {
            var equipment = new EquimpmentCreateModel
            {
                Name = request.Name,
                Description = request.Description,
                SerialNumber=request.SerialNumber,
                Price=request.Price,
                WarrantyExpiryDate= request.WarrantyExpiryDate,
                Location=request.Location,
                EquipmentType=request.EquipmentType,
                Status = request.Status,

            };
            _unitOfWork.CreateTransaction();
            // var entity = _mapper.Map<Domain.Entities.Equipment>(equipment);
            var createdEquipment=await _equipmentServiceRepository.CreateEquipment(equipment);
            if (createdEquipment == null)
            {
                _unitOfWork.Rollback();
                throw new Exception("Error occured"); //implement exception
            }

            _unitOfWork.Save();
            _unitOfWork.Commit();
            }
        }
    
}
