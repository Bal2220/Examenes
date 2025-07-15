using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Services;
using Example_EF_1.EasyIrriot.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Example_EF_1.EasyIrriot.Interfaces.REST
{
    [Route("api/v1/thing-states")]
    [ApiController]
    [Produces("application/json")]
    
    public class ThingStatesController : ControllerBase
    {
        private readonly IThingStatesCommandService _thingStatesCommandService;

        public ThingStatesController(IThingStatesCommandService thingStatesCommandService)
        {
            _thingStatesCommandService = thingStatesCommandService;
        }
        
        /// <summary>
        /// Add a Thing States.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/thing-states
        ///     {
        ///         "thingSerialNumber": "e8aa3ecd-2fa9-477a-a27e-9d0c2703c97a",
        ///         "currentOperationMode": 2,
        ///         "currentTemperature": 10.5,
        ///         "currentHumidity": 20.0,
        ///         "collectedAt": "2024-11-09T08:30:00"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return the thing states created</response>
        /// <response code="400">If the thing states is null</response>
        /// <response code="407">The value are not expected</response>
        
        // POST: api/v1/thing-states
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Post([FromBody] CreateThingStatesCommand command)
        {
            if (command == null) return BadRequest("The parameters are invalid");

            try
            {
                var thingState = await _thingStatesCommandService.Handle(command);
                var result = ThingStateResourceFromEntityAssembler.ToResource(thingState);
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
    }
}