using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) {
            return Ok(await _authService.Register(registerRequestDto));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto) => (
            Ok(await _authService.Login(loginRequestDto))
        );
    }
}