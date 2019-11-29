using System.Threading.Tasks;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DringSpot.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController: DringControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IMeetingPlaceRepository _repository;

        public ReviewController(ILogger<ReviewController> logger, IMeetingPlaceRepository service)
        {
            _logger = logger;
            _repository = service;
        }

        [HttpPost]
        [Route("Vote/{reviewId}")]
        public Task Vote(int reviewId, [FromBody] VoteDTO vote)
        {
            _logger.LogInformation($"{nameof(Vote)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(vote)}");
            return _repository.VoteForReview(reviewId, GetUserId(), vote.Date, vote.IsPositive);
        }

        [HttpPost]
        [Route("Review/{reviewId}")]
        public Task AddReview(int reviewId, [FromBody] ReviewDTO review)
        {
            _logger.LogInformation($"{nameof(AddReview)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(review)}");
            return _repository.AddReview(GetUserId(), reviewId, review.Text, review.Date, review.AttendeeNumber);
        }
    }
}