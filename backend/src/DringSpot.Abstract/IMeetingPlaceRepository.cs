using System.Threading.Tasks;
using DringSpot.DataAccess.Models;

namespace DringSpot.DataAccess.EF
{
    public interface IMeetingPlaceRepository
    {
        Task AddPlace(string lat, string lon, params string[] categories);
        
        Task<MeetingPlaceViewModel> GetPlaces();

        Task<MeetingPlaceViewModel> GetPlacesWithin(float range);
    }
}