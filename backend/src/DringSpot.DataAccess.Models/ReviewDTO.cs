using System;

namespace DringSpot.DataAccess.Models
{
    /// <summary>
    /// Data transportation object for review.
    /// </summary>
    public class ReviewDTO
    {
        /// <summary>
        /// Gets or sets text of review.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets date of review.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets attendee number of event (optional).
        /// </summary>
        public int? AttendeeNumber { get; set; }
    }
}