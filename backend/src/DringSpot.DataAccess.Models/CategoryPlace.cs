namespace DringSpot.DataAccess.Models
{
    public class CategoryPlace
    {
        public int CategoryId { get; set; }
        public int MeetingPlaceId { get; set; }
        public Category Category { get; set; }
        public MeetingPlace MeetingPlace { get; set; }
    }
}