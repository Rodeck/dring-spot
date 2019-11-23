using System.Collections.Generic;
using System.Threading.Tasks;
using DringSpot.Abstract;
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
        private readonly IMeetingPlaceService _service;

        public MeetingPlaceController(ILogger<MeetingPlaceController> logger, IMeetingPlaceService service)
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
        public Task<List<MeetingPlaceViewModel>> Users()
        {
            return _service.GetPlaces();
        }
    }
}