using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class MeetingPlaceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Text { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public int ReviewsCount { get; set; }
    }
}