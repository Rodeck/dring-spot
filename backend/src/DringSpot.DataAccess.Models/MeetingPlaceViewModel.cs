using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    /// <summary>
    /// View model for <see cref="MeetingPlace"/>.
    /// </summary>
    public class MeetingPlaceViewModel
    {
        /// <summary>
        /// Gets or sets id of place.
        /// </summary>
        public int Id { get; set; }
 
        /// <summary>
        /// Gets or sets name of place.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Lattitude of place.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets longitude of place.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets text description of place.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets categories of place <see cref="CategoryDTO"/>.
        /// </summary>
        public ICollection<CategoryDTO> Categories { get; set; }

        /// <summary>
        /// Gets or sets reviews of place <see cref="ReviewViewModel"/>.
        /// </summary>
        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        /// <summary>
        /// Gets or sets count of reviews.
        /// </summary>
        public int ReviewsCount { get; set; }
    }
}