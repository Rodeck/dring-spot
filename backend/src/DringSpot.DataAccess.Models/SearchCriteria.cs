namespace DringSpot.DataAccess.Models
{
    /// <summary>
    /// Criteria for searching places.
    /// </summary>
    public class SearchCriteria
    {
        /// <summary>
        /// Latitude.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// Given range.
        /// </summary>
        public double Range { get; set; }

        /// <summary>
        /// Containing this name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// With all those categories.
        /// </summary>
        public string[] Categories { get; set; }
    }
}