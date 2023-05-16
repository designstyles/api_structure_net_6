using Backend.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<WeatherForecastController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Returns a valid token model 
        /// TOKEN to be attached to all protected api endpoints
        /// </summary>
        /// <returns>String token, if credentials are validated</returns>
        [HttpGet]
        [Route("Authenticate")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Authenticate(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tokenData = _authService.Generate(email, password);
            return Ok(tokenData);
        }
    }
}