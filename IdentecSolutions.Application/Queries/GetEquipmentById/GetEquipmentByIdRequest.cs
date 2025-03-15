using IdentecSolutions.Application.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IdentecSolutions.Application.Queries.GetEquipmentById
{
    public sealed record GetEquipmentByIdRequest : BaseQuery<GetEquipmentByIdResponse>
    {
        [FromQuery(Name = "id")]
        public int Id { get; set; }
    }
}
