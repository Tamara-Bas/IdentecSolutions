using IdentecSolutions.Application.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public sealed record GettAllEquipmentByIdRequest : BaseQuery<GetAllEquipmentByIdResponse>
    {
        [FromQuery(Name="id")]
        public int Id { get; set; }
    }
}
