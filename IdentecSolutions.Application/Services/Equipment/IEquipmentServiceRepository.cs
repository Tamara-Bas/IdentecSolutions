using IdentecSolutions.Application.Models.Equipment;

namespace IdentecSolutions.Application.Services.Equipment
{
    public interface IEquipmentServiceRepository
    {
        Task<List<Domain.Entities.Equipment>> GetAllEquipmentByStatus(bool status, CancellationToken cancellation);
        Task<Domain.Entities.Equipment> GetEquipmentById(int equipmentId, CancellationToken cancellation);

        Task<Domain.Entities.Equipment> CreateEquipment(EquimpmentCreateModel equipment);
    }
}
