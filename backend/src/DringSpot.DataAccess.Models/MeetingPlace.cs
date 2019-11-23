using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class MeetingPlace
    {
        public int Id { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
        
        public ICollection<Review> Reviews { get; set; } 

        public ICollection<Votee> Votees { get; set; } 
    }
}