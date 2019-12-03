using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.Abstract;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using DringSpot.WebApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DringSpot.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling operations for <see cref="MeetingPlace"/>.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MeetingPlaceController: DringControllerBase
    {
        private readonly ILogger<MeetingPlaceController> _logger;
        private readonly IMeetingPlaceRepository _repository;

        /// <summary>
        /// Initializes instance of <see cref="MeetingPlaceController"/>.
        /// </summary>
        /// <param name="logger">Logger used for logging operations <see cref="ILogger{MeetingPlaceController}"/>.</param>
        /// <param name="repository">Repository for geting, creating and updating <see cref="MeetingPlace"/>.</param>
        public MeetingPlaceController(ILogger<MeetingPlaceController> logger, IMeetingPlaceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Adds new metting place.
        /// </summary>
        /// <param name="placeDTO">Data needed for creating new meeting place <see cref="MeetingPlaceDTO"/>.</param>
        /// <returns>Task</returns>
        [HttpPost]
        public Task AddPlace([FromBody] MeetingPlaceDTO placeDTO)
        {
            _logger.LogInformation($"{nameof(AddPlace)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(placeDTO)}");
            return _repository.AddPlace(GetUserId(), placeDTO.Lat, placeDTO.Lon, placeDTO.Name, placeDTO.Text, placeDTO.Categories);
        }

        /// <summary>
        /// Loads all metting places.
        /// </summary>
        /// <returns>Task containing collection of <see cref="MeetingPlaceViewModel"/>.</returns>
        [HttpGet]
        public Task<List<MeetingPlaceViewModel>> Load()
        {
            _logger.LogInformation($"{nameof(Load)} UID: {GetUserId()}");
            var uid = GetUserId();
            return _repository.GetPlaces();
        }

        /// <summary>
        /// Loads all places within given range.
        /// </summary>
        /// <param name="lat">Lattitude of cetral point.</param>
        /// <param name="lon">Longitude of centra point.</param>
        /// <param name="range">Range in meters to seek within.</param>
        /// <returns>Task containing collection of <see cref="MeetingPlaceViewModel"/>.</returns>
        [HttpGet]
        [Route("PlacesWithin/{lat:double}/{lon:double}/{range:double}")]
        public Task<List<MeetingPlaceViewModel>> GetPlaceWithin(double lat, double lon, double range)
        {
            _logger.LogInformation($"{nameof(GetPlaceWithin)} UID: {GetUserId()}, Lat: {lat}, Lng: {lon}, Range: {range}");
            return _repository.GetPlacesWithin(GetUserId() ,lat, lon, range);
        }

        /// <summary>
        /// Adds new category for meeting place. Category must exist.
        /// </summary>
        /// <param name="placeId">Place id.</param>
        /// <param name="category">Category DTO <see cref="CategoryDTO"/>.</param>
        /// <returns>Task</returns>
        [HttpPost]
        [Route("AddCategory/{placeId}")]
        public Task AddCategory(int placeId, [FromBody] CategoryDTO category)
        {
            _logger.LogInformation($"{nameof(AddCategory)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(category)}");
            return _repository.AddCategoryToPlace(GetUserId(), placeId, category.Name);
        }

        /// <summary>
        /// Returns all avaliable categories.
        /// </summary>
        /// <returns>Task containing collection of <see cref="CategoryResponseModel"/>.</returns>
        [HttpGet]
        [Route("GetCategories")]
        public IAsyncEnumerable<CategoryResponseModel> GetCategories()
        {
            _logger.LogInformation($"{nameof(GetCategories)} UID: {GetUserId()}");
            return _repository.GetCategories();
        }

        /// <summary>
        /// Get place by id.
        /// </summary>
        /// <returns>Task containing <see cref="MeetingPlaceViewModel"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<MeetingPlaceViewModel> GetPlace(int id)
        {
            var uid = GetUserId();
            _logger.LogInformation($"{nameof(GetPlace)} UID: {uid}");
            return _repository.GetPlace(id);
        }


        /// <summary>
        /// Get all places that match given criteria:
        /// - have all required categories
        /// - match searched name (this can be skipped)
        /// - are within given range of given localization
        /// </summary>
        /// <returns>Task containing list of <see cref="MeetingPlaceViewModel"/>.</returns>
        [HttpPost]
        [Route("Search")]
        public Task<List<MeetingPlaceViewModel>> SearchPlaces([FromBody] SearchCriteria criteria)
        {
            var uid = GetUserId();
            _logger.LogInformation($"{nameof(GetPlace)} UID: {uid}");
            return _repository.SearchPlaces(criteria.Lat, criteria.Lng, criteria.Range, criteria.Name, criteria.Categories);
        }
    }
}