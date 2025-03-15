using IdentecSolutions.Application.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public sealed record GettAllEquipmentByStatusRequest : BaseQuery<GetAllEquipmentByStatusResponse>
    {
        [FromQuery(Name="status")]
        public bool Status { get; set; }
    }
}
