using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class MeetingPlaceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }
    }
}