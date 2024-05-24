using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        private IRatingService _ratingService;
        public RatingService(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        public Task insertRating(Rating rating)
        {
            return _ratingService.insertRating(rating);
        }
    }
}
