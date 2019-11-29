namespace DringSpot.WebApi.DTO
{
    public class MeetingPlaceDTO
    {
        public double Lat { get; set; }

        public double Lon { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string[] Categories { get; set; }
    }
}