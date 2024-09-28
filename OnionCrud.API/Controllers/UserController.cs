using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionCrud.API.Utility;
using OnionCrud.Application.Users.DTOs;
using OnionCrud.Application.Users.Interfaces;

namespace OnionCrud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("getallusers")]
        public async Task<IActionResult> GetUsers()
        {
            var response = new Response<List<GetUser>>();

            try
            {
                response.status = true;
                response.value = await _userService.GetAllUserAsync();
                response.message = "Successful data";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);

        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> EditUser([FromBody] UpdateUser user)
        {
            var response = new Response<bool>();

            try
            {
                response.status = true;
                response.value = await _userService.UpdateAsync(user);
                response.message = "User information updated successfully";
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }

            return Ok(response);

        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            var response = new Response<bool>();

            try
            {
                response.value = await _userService.SoftDeleteAsync(id);
                response.status = true;
                response.message = "User soft deleted successfully";
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
