using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.Abstract;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using DringSpot.WebApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DringSpot.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingPlaceController: ControllerBase
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
            await _service.AddPlace(placeDTO.Lat, placeDTO.Lon, placeDTO.Name, placeDTO.Categories);
        }

        [HttpGet]
        public Task<List<MeetingPlaceViewModel>> Load()
        {
            return _service.GetPlaces();
        }

        [HttpGet]
        [Route("PlacesWithin/{lat:double}/{lon:double}/{range:double}")]
        public Task<List<MeetingPlaceViewModel>> GetPlaceWithin(double lat, double lon, double range)
        {
            return _service.GetPlacesWithin(lat, lon, range);
        }

        [HttpPost]
        [Route("AddCategory/{placeId}")]
        public Task GetPlaceWithin(int placeId, [FromBody] CategoryDTO category)
        {
            return _service.AddCategoryToPlace(placeId, category.Name);
        }

        [HttpGet]
        [Route("GetCategories")]
        public IAsyncEnumerable<CategoryResponseModel> GetCategories()
        {
            return _service.GetCategories();
        }
    }
}