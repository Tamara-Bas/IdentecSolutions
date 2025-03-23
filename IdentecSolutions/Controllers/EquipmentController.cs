using IdentecSolutions.Application.Commands.Equipment.CreateEquipment;
using IdentecSolutions.Application.Commands.Equipment.DeleteEquipment;
using IdentecSolutions.Application.Commands.Equipment.UpdateEquipment;
using IdentecSolutions.Application.Contracts;
using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Queries.GetAllEquipmentByStatus;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using IdentecSolutions.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentecSolutions.WebApi.Controllers;

[ApiController]
[Route("api/equipment")]
public class EquipmentController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public EquipmentController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
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
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = OpenApiEndpointDocumentation.GetAllEquipmentByStatusSummary, Description = OpenApiEndpointDocumentation.GetAllEquipmentByStatusDescription)]
    public async Task<IActionResult> GetEquipmentById([FromQuery] GetEquipmentByIdRequest query, CancellationToken cancellationToken)
    {
        var response = await _queryDispatcher.QueryAsync(query, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    //[Route("create-equipment")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status409Conflict)] //duplicate error
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)] //validation error
    [SwaggerOperation(Summary = OpenApiEndpointDocumentation.CreateEquipmentSummary, Description = OpenApiEndpointDocumentation.CreateEquipmentDescription)]
    public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentRequest command, CancellationToken cancellationToken)
    {
         await _commandDispatcher.SendAsync(command, CancellationToken.None).ConfigureAwait(false);
        return Ok(true);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(UpdateEquipmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)] //validation error
    [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)] //not found error
    [SwaggerOperation(Summary = OpenApiEndpointDocumentation.UpdateEquipmentSummary, Description = OpenApiEndpointDocumentation.UpdateEquipmentDescription)]
    public async Task<IActionResult> UpdateEquipment([FromRoute] UpdateEquipmentRequest command, CancellationToken cancellationToken)
    {
       var response= await _commandDispatcher.SendAsync<UpdateEquipmentResponse,UpdateEquipmentRequest>(command, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)] //not found error
    [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)] //validation error
    [SwaggerOperation(Summary = OpenApiEndpointDocumentation.DeleteEquipmentSummary, Description = OpenApiEndpointDocumentation.DeleteEquipmentDescription)]
    public async Task<IActionResult> DeleteEquipment([FromRoute]DeleteEquipmentByIdRequest command, CancellationToken cancellationToken)
    {
        bool isDeleted=await _commandDispatcher.SendAsync<bool, DeleteEquipmentByIdRequest>(command, CancellationToken.None).ConfigureAwait(false);
        return Ok(true);
    }
}
