using System.Threading.Tasks;
using DringSpot.DataAccess.Models;

namespace DringSpot.DataAccess.EF
{
    public interface IUserRepository: IRepository<User>
    {
        Task AddFriend(int userId, string friendEmail);

        Task RemoveFriend(int userId, int friendId);
    }
}