using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Queries.GetAllEquipment;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using IdentecSolutions.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentecSolutions.WebApi.Controllers;

[ApiController]
[Route("api/equipment")]
//[ApiController]
//TBD
public class EquipmentController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public EquipmentController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [Route("get-all-equipment-by-status")]
    [ProducesResponseType(typeof(GetEquipmentByIdResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = OpenApiEndpointDocumentation.GetAllEquipmentByStatusSummary, Description = OpenApiEndpointDocumentation.GetAllEquipmentByStatusDescription)]

    public async Task<IActionResult> GetAllEquipmentByStatus([FromQuery] GetAllEquipmentByStatusRequest query, CancellationToken cancellationToken)
    {
        var response = await _queryDispatcher.QueryAsync(query, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpGet]
    [Route("get-equipment-by-id")]
    [ProducesResponseType(typeof(GetEquipmentByIdResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = OpenApiEndpointDocumentation.GetEquipmentByIdSummary, Description = OpenApiEndpointDocumentation.GetEquipmentByIdDescription)]
    public async Task<IActionResult> GetEquipmentById([FromQuery] GetEquipmentByIdRequest query, CancellationToken cancellationToken)
    {
        var response = await _queryDispatcher.QueryAsync(query, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }
}
