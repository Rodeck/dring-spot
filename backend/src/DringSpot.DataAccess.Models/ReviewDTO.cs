using System;

namespace DringSpot.DataAccess.Models
{
    public class ReviewDTO
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int? AttendeeNumber { get; set; }
    }
}