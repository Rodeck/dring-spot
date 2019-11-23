using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.DataAccess.Models;

namespace DringSpot.Abstract
{
    public interface IUserRepository
    {
        Task AddFriend(int userId, string friendEmail);

        Task RemoveFriend(int userId, int friendId);

        Task Add(string email, string image, string uid, string firstName, string lastName);

        Task<IEnumerable<User>> Load();
    }
}