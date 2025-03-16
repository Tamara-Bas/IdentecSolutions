using AutoMapper;
using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.EF.UnitOfWork;

namespace IdentecSolutions.Application.Commands.Equipment.CreateEquipment
{
    public class CreateEquipmentHandler : CommandHandler<CreateEquipmentRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;
        private readonly IMapper _mapper;

        public CreateEquipmentHandler(IUnitOfWork unitOfWork, IEquipmentServiceRepository equipmentServiceRepository,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _equipmentServiceRepository = equipmentServiceRepository;
            _mapper = mapper;
        }

        public override async Task Handle(CreateEquipmentRequest request, CancellationToken cancellationToken)
        {
            var equipment = new EquimpmentCreateModel
            {
                Name = request.Name,
                Description = request.Description,
                SerialNumber=request.SerialNumber,
                Price=request.Price,
                WarrantyExpiryDate=request.WarrantyExpiryDate,
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
