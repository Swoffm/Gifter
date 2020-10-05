using System;
using Microsoft.AspNetCore.Mvc;
using Gifter.Repositories;
using Gifter.Models;

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

       [HttpGet]
       public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAllUserProfile());
        }
         

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_userProfileRepository.GetUserProfileById(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.DeleteUserProfile(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.UpdateUserProfile(profile);
            return NoContent();
        }

    }
}
