using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    internal interface IRatingRepository
    {
        public Task insertRating(Rating rating);

    }
}
