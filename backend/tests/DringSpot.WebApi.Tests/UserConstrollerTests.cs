using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using DringSpot.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace DringSpot.WebApi.Tests
{
    public class UserConstrollerTests
    {
        UserController _controller;
        IUserRepository _repository;

        List<User> _users = new List<User>()
        {
            new User() 
            {
                Email = "aaa@aaa.com"
            }
        };

        public UserConstrollerTests() 
        {
            _repository = Substitute.For<IUserRepository>();
            _repository.Collection.Returns(_users.AsQueryable<User>());
            _controller = new UserController(Substitute.For<ILogger<UserController>>(),
            _repository
            );
        }

        [Fact]
        public void GetUsers_CallsRepository()
        {
            //Given
            
            //When
            _controller.Users();
            //Then
            var call = _repository.Received(1).Collection;
        }

        [Fact]
        public void GetUsers_ReturnsDataFromRepository()
        {
            //Given
            
            //When
            var result = _controller.Users();
            //Then
            Assert.Equal(_users.Count, result.Count());
        }

        [Fact]
        public async Task AddUser_CallsRepository()
        {
            //Given
            
            //When
            await _controller.AddUser(new DTO.UserDTO() 
            {
                Email = "aaa@aaa.com",
                FirstName = "fname",
                LastName = "lname",
                ImageUrl = "imgurl"
            });
            //Then
            var call = _repository.Received(1).Add(Arg.Any<User>());
        }
    }
}