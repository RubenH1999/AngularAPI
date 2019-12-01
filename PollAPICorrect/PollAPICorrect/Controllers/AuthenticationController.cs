using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PollAPICorrect.Models;
using PollAPICorrect.Services;

namespace PollAPICorrect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.UserName, userParam.Password);
            if (user == null)
                return BadRequest(new { message = "Username or Password is incorrect" });

            return Ok(user);
        }
    }
}