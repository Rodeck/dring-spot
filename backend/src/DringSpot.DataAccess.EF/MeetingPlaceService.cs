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
    public class MeetingPlaceService : IMeetingPlaceService
    {
        private DringContext _context;
        private ILogger<MeetingPlaceService> _logger;

        public MeetingPlaceService(DringContext context, ILogger<MeetingPlaceService> logger)
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

            place.Categories.Add(category);

            await _context.SaveChangesAsync();
        }

        public async Task AddPlace(string lat, string lon, string name, params string[] categories)
        {
            var categoriesList = new List<Category>();

            foreach(var cat in categories)
            {
                categoriesList.Add(await _context.Categories.SingleAsync(x => x.Name == cat));
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

        public async Task<List<MeetingPlaceViewModel>> GetPlaces()
        {
            return (await _context.Places
                .Include(x => x.Categories)
                .Include(x => x.Reviews)
                    .ThenInclude(y => y.Votees)
                .ToListAsync())
                .Select(x => new MeetingPlaceViewModel()
                {
                    Categories = x.Categories.Select(c => new CategoryDTO() { Name = c.Name }).ToList(),
                    Id = x.Id,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Name = x.Name
                })
                .ToList();
        }

        public Task<List<MeetingPlaceViewModel>> GetPlacesWithin(float range)
        {
            throw new System.NotImplementedException();
        }

        public Task VoteForReview(int reviewId, int votee, DateTime date)
        {
            throw new System.NotImplementedException();
        }
    }
}