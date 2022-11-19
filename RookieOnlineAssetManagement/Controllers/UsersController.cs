using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Interface;
using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        

        public UsersController(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var currUser = await _userManager.GetUserAsync(User);
            return Ok(currUser);
        }
        [HttpPost]
        public async Task<ActionResult> Post(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                byte response = await _userRepository.PostAsync(model, user);
                switch (response)
                {
                    case 0:
                        return Ok("yes");
                        break;
                    case 1:
                        return BadRequest("Age must be older than 18 years old");
                        break;
                    case 2:
                        return BadRequest("JoinedDay must be later than DOB");
                        break;
                    case 3:
                        return BadRequest("Please choose gender");
                        break;
                    case 4:
                        return BadRequest("Please choose Type");
                        break;

                }

            }
            return BadRequest();

        }
    }
}
