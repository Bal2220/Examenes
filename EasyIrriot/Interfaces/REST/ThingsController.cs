using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Models.Queries;
using Example_EF_1.EasyIrriot.Domain.Services;
using Example_EF_1.EasyIrriot.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Example_EF_1.EasyIrriot.Interfaces.REST
{
    [Route("api/v1/things")]
    [ApiController]
    [Produces("application/json")]
    
    public class ThingsController : ControllerBase
    {
        private readonly IThingsCommandService _thingsCommandService;
        private readonly IThingsQueryService _thingsQueryService;
        
        public ThingsController(
            IThingsCommandService thingsCommandService,
            IThingsQueryService thingsQueryService)
        {
            _thingsCommandService = thingsCommandService;
            _thingsQueryService = thingsQueryService;
        }
        
        /// <summary>
        /// Add a Things.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/things
        ///     {
        ///         "model": "Ejemplo",
        ///         "maximumTemperatureThreshold": -10.00,
        ///         "minimumHumidityThreshold": 95.00
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return the thing created</response>
        /// <response code="400">If the thing is null</response>
        /// <response code="407">The value are not expected</response>
        
        // POST: api/v1/things
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Post([FromBody] CreateThingsCommand command)
        {
            if (command == null) return BadRequest("The parameters are invalid");
            
            try
            {
                var thing = await _thingsCommandService.Handle(command);
                var result = ThingResourceFromEntityAssembler.ToResource(thing);
                return StatusCode(201, result);
            }
            catch (ArgumentException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        
        /// <summary>
        /// Get a thing by its ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/things/1
        ///
        /// </remarks>
        /// <param name="id">The ID of the thing to retrieve</param>
        /// <response code="200">Returns the thing with the specified ID</response>
        /// <response code="400">If the ID is invalid</response>
        /// <response code="404">If the thing is not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/v1/things/{id}
        //[HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Invalid book ID.");

            var result = await _thingsQueryService.Handle(new GetThingsByIdQuery(id));
            if (result == null)
                return NotFound();

            var resource = ThingResourceFromEntityAssembler.ToResource(result);
            return Ok(resource);
        }
    }
}