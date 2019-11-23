using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DringSpot.Abstract;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using DringSpot.WebApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DringSpot.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _repository;

        public UserController(ILogger<UserController> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task AddUser([FromBody] UserDTO userData)
        {
            await _repository.Add(userData.Email, userData.ImageUrl, userData.Uid, userData.FirstName, userData.LastName);
        }

        [HttpGet]
        public Task<IEnumerable<User>> Users()
        {
            return _repository.Load();
        }

        [HttpPost]
        [Route("Friend/Add")]
        public Task AddFriend([FromBody]AddFriendDTO friendDTO)
        {
            return _repository.AddFriend(friendDTO.UserId, friendDTO.FriendEmail);
        }
    }
}