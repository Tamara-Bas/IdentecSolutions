using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.EF.UnitOfWork;

namespace IdentecSolutions.Application.Commands.Equipment.DeleteEquipment
{
    public class DeleteEquipmentByIdHandler : ICommandHandler<DeleteEquipmentByIdRequest,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;

        public DeleteEquipmentByIdHandler(IUnitOfWork unitOfWork, IEquipmentServiceRepository equipmentServiceRepository)
        {
            _unitOfWork = unitOfWork;
            _equipmentServiceRepository = equipmentServiceRepository;
        }
        public async Task<bool> Handle(DeleteEquipmentByIdRequest request, CancellationToken cancellationToken)
        {
            var equipment = await _equipmentServiceRepository.GetEquipmentById(request.Id, cancellationToken);

            if (equipment == null)
            {
                throw new Exception("Equipment not found");
                    //to do: implement exception not found
            }

            _unitOfWork.CreateTransaction();

            var isdDeletedEquipment = await _equipmentServiceRepository.DeleteEquipment(equipment.Id, cancellationToken);

            if (!isdDeletedEquipment)
            {
                throw new Exception("Failed to delete equipment");
            }

            _unitOfWork.Save();
            _unitOfWork.Commit();

            return isdDeletedEquipment;
        }
    }
}
