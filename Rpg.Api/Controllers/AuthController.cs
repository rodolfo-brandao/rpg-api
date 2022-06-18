using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rpg.Application.Requests.Auth;
using Rpg.Application.Responses.Auth;
using Rpg.Application.Util;

namespace Rpg.Api.Controllers
{
    /// <summary>
    /// Controller to handle authentication requests.
    /// </summary>
    [ApiController, ApiVersion("1"), Produces(ContentTypes.Json)]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Public constructor to initialize a instance of <see cref="IMediator"/>.
        /// </summary>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Grants access to a player.
        /// </summary>
        /// <param name="request">The request body properties.</param>
        /// <response code="200">Player access granted.</response>
        /// <response code="400">There were some problems when trying to log in the player.</response>
        [AllowAnonymous]
        [HttpPost("login", Name = "login")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(LoginResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(IEnumerable<string>))]
        public async Task<IActionResult> PostAsync([FromBody] LoginRequest request)
        {
            var apiResult = await _mediator.Send(request);

            if (!apiResult.IsSuccessfulRequest)
            {
                return BadRequest(apiResult.ErrorMessages);
            }

            return Ok(apiResult.Response);
        }
    }
}
