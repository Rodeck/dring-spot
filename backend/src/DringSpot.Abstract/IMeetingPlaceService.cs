namespace DringSpot.Abstract
{
    public interface IMeetingPlaceService
    {
        Task CreateCategory(string name, string icon);

        Task AddCategoryToPlace(int placeId, int catergoryId);

        Task AddReview(int placeId, string text, int reviewerId, DateTime date, int attendeeNumber);

        Task VoteForReview(int reviewId, int votee, DateTime date);
    }
}