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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MeetingPlaceController: DringControllerBase
    {
        private readonly ILogger<MeetingPlaceController> _logger;
        private readonly IMeetingPlaceRepository _service;

        public MeetingPlaceController(ILogger<MeetingPlaceController> logger, IMeetingPlaceRepository service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task AddPlace([FromBody] MeetingPlaceDTO placeDTO)
        {
            _logger.LogInformation($"{nameof(AddPlace)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(placeDTO)}");
            await _service.AddPlace(GetUserId(), placeDTO.Lat, placeDTO.Lon, placeDTO.Name, placeDTO.Text, placeDTO.Categories);
        }

        [HttpGet]
        public Task<List<MeetingPlaceViewModel>> Load()
        {
            _logger.LogInformation($"{nameof(Load)} UID: {GetUserId()}");
            var uid = GetUserId();
            return _service.GetPlaces();
        }

        [HttpGet]
        [Route("PlacesWithin/{lat:double}/{lon:double}/{range:double}")]
        public Task<List<MeetingPlaceViewModel>> GetPlaceWithin(double lat, double lon, double range)
        {
            _logger.LogInformation($"{nameof(GetPlaceWithin)} UID: {GetUserId()}, Lat: {lat}, Lng: {lon}, Range: {range}");
            return _service.GetPlacesWithin(GetUserId() ,lat, lon, range);
        }

        [HttpPost]
        [Route("AddCategory/{placeId}")]
        public Task AddCategory(int placeId, [FromBody] CategoryDTO category)
        {
            _logger.LogInformation($"{nameof(AddCategory)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(category)}");
            return _service.AddCategoryToPlace(GetUserId(), placeId, category.Name);
        }

        [HttpGet]
        [Route("GetCategories")]
        public IAsyncEnumerable<CategoryResponseModel> GetCategories()
        {
            _logger.LogInformation($"{nameof(GetCategories)} UID: {GetUserId()}");
            return _service.GetCategories();
        }
    }
}