using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.DataAccess.Models;

namespace DringSpot.Abstract
{
    public interface IMeetingPlaceService
    {
        Task AddCategoryToPlace(int placeId, string catergoryName);

        Task AddReview(int placeId, string text, int reviewerId, DateTime date, int attendeeNumber);

        Task VoteForReview(int reviewId, int votee, DateTime date);

        Task AddPlace(string lat, string lon, string name, params string[] categories);
        
        Task<List<MeetingPlaceViewModel>> GetPlaces();

        Task<List<MeetingPlaceViewModel>> GetPlacesWithin(float range);
    }
}