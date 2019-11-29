using System;
using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class MeetingPlace
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<CategoryPlace> Categories { get; set; }
        
        public virtual ICollection<Review> Reviews { get; set; } 

        public virtual ICollection<Votee> Votees { get; set; } 

        public MeetingPlace()
        {
            Categories = new HashSet<CategoryPlace>();
            Reviews = new HashSet<Review>();
            Votees = new HashSet<Votee>();
        }
    }
}