using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using DringSpot.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DringSpot.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController: ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly List<User> _users = new List<User>();

        public TestController(ILogger<UserController> logger, IUserRepository repository)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task AddUser([FromBody] UserDTO userData)
        {
            _users.Add(new User() 
            {
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                ImageUrl = userData.ImageUrl
            });
        }

        [HttpGet]
        public IEnumerable<User> Users()
        {
            return _users;
        }
    }
}