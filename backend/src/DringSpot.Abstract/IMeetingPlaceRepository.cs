using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.DataAccess.Models;

namespace DringSpot.DataAccess.EF
{
    public interface IMeetingPlaceRepository
    {
        Task AddCategoryToPlace(int placeId, string catergoryName);

        Task AddReview(int placeId, string text, int reviewerId, DateTime date, int attendeeNumber);

        Task AddPlace(double lat, double lon, string name, params string[] categories);

        Task<List<MeetingPlaceViewModel>> GetPlaces();

        Task<List<MeetingPlaceViewModel>> GetPlacesWithin(double lat, double lon, double distance);

        IAsyncEnumerable<CategoryResponseModel> GetCategories();
    }
}