using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.DataAccess.Models;

namespace DringSpot.DataAccess.EF
{
    public interface IMeetingPlaceRepository
    {
        Task AddCategoryToPlace(string userId, int placeId, string catergoryName);

        Task AddReview(string userId, int placeId, string text, DateTime date, int? attendeeNumber);

        Task AddPlace(string userId, double lat, double lon, string name, string text, params string[] categories);

        Task<List<MeetingPlaceViewModel>> GetPlaces();

        Task<MeetingPlaceViewModel> GetPlace(int id);

        Task<List<MeetingPlaceViewModel>> GetPlacesWithin(string userId, double lat, double lon, double distance);

        IAsyncEnumerable<CategoryResponseModel> GetCategories();

        Task VoteForReview(int reviewId, string votee, DateTime date, bool isPositive);
    }
}