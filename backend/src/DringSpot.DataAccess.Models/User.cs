using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class User 
    {
        public User() 
        {
            Friends = new List<Friend>();
            FavouredPlaces = new List<FavouredPlace>();
        }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Email { get; set; }

        public string Uid { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }

        public virtual ICollection<FavouredPlace> FavouredPlaces { get; set; }
    }
}