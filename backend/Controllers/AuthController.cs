using backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using System.Security.Authentication;
using NuGet.Common;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new BadHttpRequestException("User can't be null");
                }
                var createdId = await _userService.Register(request);
                return Ok(createdId); 
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
               
           }

           [HttpPost("login")]
           public async Task<IActionResult> Login(UserRequest request)
           {
            try
            {
                if (request == null)
                {
                    throw new BadHttpRequestException("User can't be null");
                }
                var login = await _userService.Login(request);

                return Ok(login); 
          
            }
            catch (AuthenticationException e)
            {
                return StatusCode(500, e.Message); 
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
                 
            }

        
    }
}