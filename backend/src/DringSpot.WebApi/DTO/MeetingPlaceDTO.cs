namespace DringSpot.WebApi.DTO
{
    /// <summary>
    /// Data transportation object containing all needed information to construct <see cref="MeetingPlace"/>.
    /// </summary>
    public class MeetingPlaceDTO
    {
        /// <summary>
        /// Gets or sets Lattitude.
        /// </summary>
        public double Lat { get; set; }

        // <summary>
        /// Gets or sets Longitude.
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// Gets or sets name of the place.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description text for place.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets array of categories for new place.
        /// </summary>
        public string[] Categories { get; set; }
    }
}