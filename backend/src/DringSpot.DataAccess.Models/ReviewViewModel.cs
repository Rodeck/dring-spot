using System;

namespace DringSpot.DataAccess.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Reviewer { get; set; }

        public int Points { get; set; }

        public string Text { get; set; }
    }
}