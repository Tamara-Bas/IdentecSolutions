using IdentecSolutions.Application.Contracts;
using IdentecSolutions.Application.Models.Equipment;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public class GetAllEquipmentByStatusResponse :Response<List<EquipmentDto>>
    {
        public GetAllEquipmentByStatusResponse(List<EquipmentDto> data)
        {
            Data = data;
        }
    }
}
