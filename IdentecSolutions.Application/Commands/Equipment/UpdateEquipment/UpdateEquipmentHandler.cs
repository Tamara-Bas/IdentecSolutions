using AutoMapper;
using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.EF.UnitOfWork;

namespace IdentecSolutions.Application.Commands.Equipment.UpdateEquipment
{
    public class UpdateEquipmentHandler : ICommandHandler<UpdateEquipmentRequest, UpdateEquipmentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;
        private readonly IMapper _mapper;

        public UpdateEquipmentHandler(IUnitOfWork unitOfWork, IEquipmentServiceRepository equipmentServiceRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _equipmentServiceRepository = equipmentServiceRepository;
            _mapper = mapper;
        }

        public async Task<UpdateEquipmentResponse> Handle(UpdateEquipmentRequest request, CancellationToken cancellationToken)
        {
            var equipment = await _equipmentServiceRepository.GetEquipmentById(request.Id, cancellationToken) ?? 
                throw new Exception("Equipment not found");

            //equipment.Status = request.Properties.Status;

            _unitOfWork.CreateTransaction();

            // var entity = _mapper.Map<UpdateEquipmentModel>(equipment);
            var updateEquipmentModel = new UpdateEquipmentModel
            {
                Status = request.Properties.Status

            };
            var updatedEquipment = await _equipmentServiceRepository.UpdateEquipment(updateEquipmentModel, cancellationToken);

            if (updatedEquipment==null)
            {
                throw new Exception("Failed to update equipment");
            }

            var updatedMappedEquipment = _mapper.Map<EquipmentDto>(updatedEquipment);

            _unitOfWork.Save();
            _unitOfWork.Commit();

            return new UpdateEquipmentResponse(updatedMappedEquipment);
        }
    }
}
