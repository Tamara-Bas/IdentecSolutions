using IdentecSolutions.Application.Contracts;
using IdentecSolutions.Application.Models.Equipment;

namespace IdentecSolutions.Application.Queries.GetAllEquipmentByStatus
{
    public class GetAllEquipmentByStatusResponse : PaginatedResponse<List<EquipmentDto>>
    {
        public GetAllEquipmentByStatusResponse(List<EquipmentDto> data, int totalRecord)
        {
            Data = data;
            Total = totalRecord;
        }
    }
}
