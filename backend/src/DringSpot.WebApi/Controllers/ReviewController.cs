using System.Threading.Tasks;
using DringSpot.DataAccess.EF;
using DringSpot.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DringSpot.WebApi.Controllers
{
    /// <summary>
    /// Controller for handling actions for <see cref="Review"/>.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController: DringControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly IMeetingPlaceRepository _repository;

        /// <summary>
        /// Initializes new instance of <see cref="ReviewController"/>.
        /// </summary>
        /// <param name="logger">Logger <see cref="ILogger{ReviewController}"/>.</param>
        /// <param name="repository">Repository for handling actions <see cref="IMeetingPlaceRepository"/>.</param>
        public ReviewController(ILogger<ReviewController> logger, IMeetingPlaceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Vote for review.
        /// </summary>
        /// <param name="reviewId">Id of review to vote for.</param>
        /// <param name="vote">Vote DTO <see cref="VoteDTO"/>.</param>
        /// <returns>Task.</returns>
        [HttpPost]
        [Route("Vote/{reviewId}")]
        public Task Vote(int reviewId, [FromBody] VoteDTO vote)
        {
            _logger.LogInformation($"{nameof(Vote)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(vote)}");
            return _repository.VoteForReview(reviewId, GetUserId(), vote.Date, vote.IsPositive);
        }

        /// <summary>
        /// Adds new review.
        /// </summary>
        /// <param name="placeId">Place Id.</param>
        /// <param name="review">Review DTO <see cref="ReviewDTO"/>.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Review/{placeId}")]
        public Task AddReview(int placeId, [FromBody] ReviewDTO review)
        {
            _logger.LogInformation($"{nameof(AddReview)} UID: {GetUserId()}, JSON: {JsonConvert.SerializeObject(review)}");
            return _repository.AddReview(GetUserId(), placeId, review.Text, review.Date, review.AttendeeNumber);
        }
    }
}