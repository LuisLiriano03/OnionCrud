using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionCrud.API.Utility;
using OnionCrud.Application.Authentication.DTOs;
using OnionCrud.Application.Authentication.Interfaces;
using OnionCrud.Application.Users.DTOs;

namespace OnionCrud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login([FromBody] LoginRequest login)
        {
            var response = new Response<LoginResponse>();

            try
            {
                response.status = true;
                response.value = await _authService.Login(login.Email, login.PasswordHash);
                response.message = "User logged in successfully";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register([FromBody] CreateUser user)
        {
            var response = new Response<GetUser>();

            try
            {
                response.status = true;
                response.value = await _authService.Register(user);
                response.message = "Registration successful";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);

        }



    }
}
