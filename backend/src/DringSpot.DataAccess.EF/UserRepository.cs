using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DringSpot.Abstract;
using DringSpot.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DringSpot.DataAccess.EF
{
    public class UserRepository : IUserRepository
    {
        private readonly DringContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DringContext context, ILogger<UserRepository> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<User> Collection 
        {
            get 
            {
                return _context
                .Users
                .Include(x => x.Friends)
                .AsQueryable<User>();
            }
            set 
            {

            }
        }

        public async Task Add(User entity)
        {
            _logger.LogInformation("Adding user");
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddFriend(int userId, string friendEmail)
        {
            var friend = _context.Users.SingleOrDefault(x => x.Email == friendEmail)
                ?? throw new System.Exception("User not found!");
            
            var user = await _context.Users
                .FindAsync(userId);

            user.Friends.Add(new Friend() {
                FriendId = friend.Uid
            });

            await _context.SaveChangesAsync();
        }

        public Task RemoveFriend(int userId, int friendId)
        {
            throw new System.NotImplementedException();
        }

        public async Task Add(string email, string image, string uid, string firstName, string lastName)
        {
            await _context.Users.AddAsync(new User() 
            {
                Email = email,
                FirstName = firstName,
                ImageUrl = image,
                Uid = uid,
                LastName = lastName,
            });

            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<User>> Load() 
        {
            return Task.FromResult(_context.Users.ToList().AsEnumerable());
        }
    }
}