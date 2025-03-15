using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.EF.Repository;

namespace IdentecSolutions.Application.Services.Equipment
{
    public interface IEquipmentServiceRepository
    {
        Task<EquipmentDto> GetAllEquipmentById(int equipmentId, CancellationToken cancellation);
    }
}
