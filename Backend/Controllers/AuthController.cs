using IdentityTest.Data;
using IdentityTest.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request.Email, request.Username, request.Password, "User", "Contractor");

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.Username, result.Success));
        }

        [HttpPost]
        [Route("RegisterAdvertiser")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAdvertiser(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request.Email, request.Username, request.Password, "User", "Advertiser");

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.Username, result.Success));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(request.Email, request.Password);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(new AuthResponse(result.Token, result.Username, result.Success));
        }

        private void AddErrors(AuthResult result)
        {
            foreach (var error in result.GetErrors())
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
