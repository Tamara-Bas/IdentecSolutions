using IdentecSolutions.Application.Core.Commands;
using Microsoft.AspNetCore.Mvc;

namespace IdentecSolutions.Application.Commands.Equipment.DeleteEquipment
{
    public sealed record DeleteEquipmentByIdRequest : BaseCommand<bool>
    {
        [FromRoute(Name ="id")]
        public int Id { get; set; }
    }
}
