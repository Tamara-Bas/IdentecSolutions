using IdentecSolutions.Application.Core.Commands;
using Microsoft.AspNetCore.Mvc;

namespace IdentecSolutions.Application.Commands.Equipment.UpdateEquipment
{
    public sealed record UpdateEquipmentRequest : BaseCommand<UpdateEquipmentResponse>
    {
        [FromRoute(Name ="id")]
        public int Id { get; set; }

        [FromBody]
        public UpdateEquipmentBodyRequest Properties { get; set; }
    }
}
