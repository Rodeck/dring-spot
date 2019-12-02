using System;

namespace DringSpot.DataAccess.Models
{
    /// <summary>
    /// View model for review of place.
    /// </summary>
    public class ReviewViewModel
    {
        /// <summary>
        /// Gets or sets Id of review.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets review date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets user id of reviewer.
        /// </summary>
        public string Reviewer { get; set; }

        /// <summary>
        /// Gets or sets points of this review.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets text of this review.
        /// </summary>
        public string Text { get; set; }
    }
}