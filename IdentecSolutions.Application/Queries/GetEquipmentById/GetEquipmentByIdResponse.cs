using IdentecSolutions.Application.Contracts;
using IdentecSolutions.Application.Models.Equipment;

namespace IdentecSolutions.Application.Queries.GetEquipmentById
{
    public class GetEquipmentByIdResponse : Response<EquipmentDto>
    {
        public GetEquipmentByIdResponse(EquipmentDto data)
        {
            Data = data;
        }

    }
}
