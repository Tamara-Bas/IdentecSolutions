using IdentecSolutions.Application.Contracts;
using IdentecSolutions.Application.Models.Equipment;

namespace IdentecSolutions.Application.Commands.Equipment.UpdateEquipment
{
    public class UpdateEquipmentResponse : Response<EquipmentDto>
    {
        public UpdateEquipmentResponse(EquipmentDto data)
        {
            Data = data;
        }
    }
}
