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
        
        public async Task AddCategoryToPlace(int placeId, string catergoryName)
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

            place.Categories.Add(new CategoryPlace() { Category = category });

            await _context.SaveChangesAsync();
        }

        public async Task AddPlace(double lat, double lon, string name, params string[] categories)
        {
            var categoriesList = new List<CategoryPlace>();

            foreach(var cat in categories)
            {
                var category = await _context.Categories.SingleAsync(x => x.Name == cat);
                categoriesList.Add(new CategoryPlace() { Category = category } );
            }

            await _context.Places.AddAsync(new MeetingPlace() 
            {
                Latitude = lat,
                Longitude = lon,
                Name = name,
                Categories = categoriesList,
            });

            await _context.SaveChangesAsync();
        }

        public async Task AddReview(int placeId, string text, int reviewerId, DateTime date, int attendeeNumber)
        {
            var existingReview = await _context.Reviews.SingleOrDefaultAsync(x => x.MeetingPlaceId == placeId && x.ReviewerId == reviewerId);

            if (existingReview != null)
                throw new Exception("Review already exisits!");

            var review = new Review() 
            {
                AttendeeNumber = attendeeNumber,
                Date = date,
                MeetingPlaceId = placeId,
                Points = 1,
                ReviewerId = reviewerId,
                Text = text,
                Votees = new List<Votee>() 
                {
                    new Votee() { Date = date, IsPositive = true }
                }
            };

            await _context.Reviews.AddAsync(review);
        }

        public IAsyncEnumerable<CategoryResponseModel> GetCategories()
        {
            return _context.Categories.Select(x => new CategoryResponseModel() { Name = x.Name, Icon = x.Icon }).AsAsyncEnumerable();
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
                .Select(x => new MeetingPlaceViewModel()
                {
                    Categories = x.Categories.ToList().Select(c => new CategoryDTO() { Name = c.Category.Name }).ToList(),
                    Id = x.Id,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Name = x.Name
                })
                .ToList();
        }

        public async Task<List<MeetingPlaceViewModel>> GetPlacesWithin(double lat, double lon, double dist)
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

        public Task VoteForReview(int reviewId, int votee, DateTime date)
        {
            throw new System.NotImplementedException();
        }
    }
}