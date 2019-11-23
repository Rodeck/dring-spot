namespace DringSpot.WebApi.DTO
{
    public class MeetingPlaceDTO
    {
        public string Lat { get; set; }

        public string Lon { get; set; }

        public string Name { get; set; }

        public string[] Categories { get; set; }
    }
}