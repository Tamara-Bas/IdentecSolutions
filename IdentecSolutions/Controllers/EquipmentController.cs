using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Queries.GetAllEquipment;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> GetAllEquipmentById([FromQuery] GettAllEquipmentByIdRequest query, CancellationToken cancellationToken)
    {
        var response = await _queryDispatcher.QueryAsync(query, CancellationToken.None).ConfigureAwait(false);
        return Ok(response);
    }
}
