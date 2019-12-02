using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DringSpot.Abstract;
using DringSpot.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DringSpot.DataAccess.EF
{
    public class MeetingPlaceRepository : IMeetingPlaceRepository
    {
        private DringContext _context;
        private ILogger<MeetingPlaceRepository> _logger;

        public MeetingPlaceRepository(DringContext context, ILogger<MeetingPlaceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task AddCategoryToPlace(string userId, int placeId, string catergoryName)
        {
            var place = await _context.Places.
                Include(x => x.Categories) 
                .SingleAsync(x => x.Id == placeId);

            if (place == null)
                throw new Exception($"Place with id: { placeId } not found!");

            var category = await _context.Categories
                .SingleAsync(x => x.Name == catergoryName);

            if (category == null)
                throw new Exception($"Category with name: { catergoryName } not found!");

            place.Categories.Add(new CategoryPlace() { Category = category, UserId = userId });

            await _context.SaveChangesAsync();
        }

        public async Task AddPlace(string userId, double lat, double lon, string name, string text, params string[] categories)
        {
            var categoriesList = new List<CategoryPlace>();

            foreach(var cat in categories)
            {
                var category = await _context.Categories.SingleAsync(x => x.Name == cat);
                categoriesList.Add(new CategoryPlace() { Category = category, UserId = userId, } );
            }

            await _context.Places.AddAsync(new MeetingPlace() 
            {
                Latitude = lat,
                Longitude = lon,
                Name = name,
                Categories = categoriesList,
                Text = text,
                UserId = userId,
                CreatedOn = DateTime.Now,
            });

            await _context.SaveChangesAsync();
        }

        public async Task AddReview(string userId, int placeId, string text, DateTime date, int? attendeeNumber)
        {
            var existingReview = await _context.Reviews.SingleOrDefaultAsync(x => x.MeetingPlaceId == placeId && x.ReviewerId == userId);

            if (existingReview != null)
                throw new Exception("Review already exisits!");

            var review = new Review() 
            {
                AttendeeNumber = attendeeNumber,
                Date = date,
                MeetingPlaceId = placeId,
                Points = 1,
                ReviewerId = userId,
                Text = text,
                Votees = new List<Votee>() 
                {
                    new Votee() { Date = date, IsPositive = true, UserId = userId }
                }
            };

            await _context.Reviews.AddAsync(review);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IAsyncEnumerable<CategoryResponseModel> GetCategories()
        {
            return _context.Categories.Select(x => new CategoryResponseModel() { Name = x.Name, Icon = x.Icon }).AsAsyncEnumerable();
        }

        public async Task<MeetingPlaceViewModel> GetPlace(int id)
        {
            var place = await _context
                .Places
                .Include(x => x.Categories)
                    .ThenInclude(c => c.Category)
                .Include(x => x.Reviews)
                    .ThenInclude(y => y.Votees)
                .SingleAsync(x => x.Id == id);
            return Map(place);
        }

        public async Task<List<MeetingPlaceViewModel>> GetPlaces()
        {
            var places = await _context.Places
                .Include(x => x.Categories)
                    .ThenInclude(c => c.Category)
                .Include(x => x.Reviews)
                    .ThenInclude(y => y.Votees)
                .ToListAsync();

            return places
                .Select(x => Map(x))
                .ToList();
        }

        private MeetingPlaceViewModel Map(MeetingPlace from) 
        {
            if (from == null)
                return null;

            var reviews = from.Reviews.ToList();
            return new MeetingPlaceViewModel()
            {
                Categories = from.Categories.ToList().Select(c => new CategoryDTO() { Name = c.Category.Name }).ToList(),
                Id = from.Id,
                Latitude = from.Latitude,
                Longitude = from.Longitude,
                Name = from.Name,
                Text = from.Text,
                Reviews = reviews.Select(x => new ReviewViewModel()
                {
                    Date = x.Date,
                    Id = x.Id,
                    Points = x.Points,
                    Reviewer = x.ReviewerId,
                    Text = x.Text
                }),
                ReviewsCount = reviews.Count
            };
        }

        public async Task<List<MeetingPlaceViewModel>> GetPlacesWithin(string userId, double lat, double lon, double dist)
        {
            var placesIds = await _context.GetPlacesWithin(lat, lon, dist).Select(x => x.Id).ToListAsync();

            return (await _context.Places
                .Include(x => x.Categories)
                .Where(x => placesIds.Contains(x.Id)).ToListAsync()).Select(x => 
                new MeetingPlaceViewModel()
                {
                    Id = x.Id,
                    Categories = x.Categories.Select(y => new CategoryDTO() { Name = y.Category.Name }).ToList(),
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Name = x.Name,
                }).ToList();
        }

        public async Task VoteForReview(int reviewId, string votee, DateTime date, bool isPositive)
        {
            var review = await _context.Reviews
                    .Include(x => x.Votees)
                    .SingleAsync(x => x.Id == reviewId);

            if (review == null)
                throw new Exception("");

            var existingVote = review.Votees.SingleOrDefault(x => x.UserId == votee);
            
            if (existingVote != null)
            {
                if (existingVote.IsPositive == isPositive)
                    throw new Exception($"You cannot vote twice.");
                else
                {
                    if (isPositive)
                        review.Points += 2;
                    else
                        review.Points -= 2;

                    existingVote.IsPositive = isPositive;
                }
            }
            else
            {
                review.Votees.Add(new Votee() { UserId = votee, IsPositive = isPositive });
                review.Points = isPositive ? review.Points + 1 : review.Points - 1;
            }

            await _context.SaveChangesAsync();
        }
    }
}