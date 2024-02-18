using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Api.Controllers
{
    [Route("api/participants")]
    [Produces("application/json")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ParticipantController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Get participants to create project or task
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        public ActionResult<List<ParticipantForGetDto>> GetParticipants()
        {
            var participantsFromDb = _userManager.Users;
            var participants = _mapper.Map<List<ParticipantForGetDto>>(participantsFromDb);
            return Ok(participants);
        }
    }
}
