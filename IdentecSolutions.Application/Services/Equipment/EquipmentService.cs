using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.EF.Repository;

namespace IdentecSolutions.Application.Services.Equipment
{
    public class EquipmentService : IEquipmentServiceRepository
    {
        private readonly IBaseRepository<Domain.Entities.Equipment> _equipmentRepository;
        public EquipmentService(IBaseRepository<Domain.Entities.Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        public Task<EquipmentDto> GetAllEquipmentById(int equipmentId, CancellationToken cancellation)
        {
            var equipment = _equipmentRepository.FirstOrDefault(x => x.Id == equipmentId);
            return null;
        }
    }
}
