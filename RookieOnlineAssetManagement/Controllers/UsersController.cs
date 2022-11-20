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
        private readonly SignInManager<User> _signInManager;


        public UsersController(UserManager<User> userManager, IUserRepository userRepository, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
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
                        return BadRequest("User is under 18. Please select a different date");
                        break;
                    case 2:
                        return BadRequest("Joined date is not later than Date of Birth.Please select a different date");
                        break;
                    case 3:
                        return BadRequest("Please choose gender");
                        break;
                    case 4:
                        return BadRequest("Please choose Type");
                        break;
                    case 5:
                        return BadRequest("Joined date is Saturday or Sunday. Please select a different date");
                        break;

                }

            }
            return BadRequest();

        }
        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
