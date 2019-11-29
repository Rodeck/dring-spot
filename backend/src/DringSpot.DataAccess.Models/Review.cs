using System;
using System.Collections.Generic;

namespace DringSpot.DataAccess.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int MeetingPlaceId { get; set; }

        public string Text { get; set; }

        public string ReviewerId { get; set; }

        public DateTime Date { get; set; }

        public int? AttendeeNumber { get; set; }

        public int Points { get; set; }

        public ICollection<Votee> Votees { get; set; }
    }
}