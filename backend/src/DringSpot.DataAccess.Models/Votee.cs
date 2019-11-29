using System;

namespace DringSpot.DataAccess.Models
{
    public class Votee
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public bool IsPositive { get; set; }
    }
}