using IdentecSolutions.Application.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public sealed record GetAllEquipmentByStatusRequest : BaseQuery<GetAllEquipmentByStatusResponse>
    {
        [FromQuery(Name="status")]
        public bool Status { get; set; }
    }
}
