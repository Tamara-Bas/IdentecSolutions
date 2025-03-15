using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Queries.GetAllEquipment;
using IdentecSolutions.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentecSolutions.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
//[Route("api/[controller]")]
//[ApiController]
//TBD
public class EquipmentController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public EquipmentController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet(Name = "GetAllEquipmentByStatus")]
    public async Task<IActionResult> GetAllEquipmentByStatus([FromQuery] GettAllEquipmentByStatusRequest query, CancellationToken cancellationToken)
    {
        var response = await _queryDispatcher.QueryAsync(query, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpGet(Name = "GetEquipmentById")]
    [Route("get-equipment-by-id")]
    [ProducesResponseType(typeof(GetAllEquipmentByStatusResponse),StatusCodes.Status200OK)]
    [SwaggerOperation(Summary =OpenApiEndpointDocumentation.GetAllEquipmentByStatusSummary,Description =OpenApiEndpointDocumentation.GetAllEquipmentByStatusDescription)]
    public async Task<IActionResult> GetEquipmentById([FromQuery] GettAllEquipmentByStatusRequest query, CancellationToken cancellationToken)
    {
        var response = await _queryDispatcher.QueryAsync(query, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }
}
