using System;

namespace DringSpot.DataAccess.Models
{
    /// <summary>
    /// Data transportation object for vote.
    /// </summary>
    public class VoteDTO
    {
        /// <summary>
        /// Gets or sets date of vote.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets if vote is positive.
        /// </summary>
        public bool IsPositive { get; set; }
    }
}