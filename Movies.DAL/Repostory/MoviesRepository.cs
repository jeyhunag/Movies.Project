using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Repostory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repostory
{
    public class MoviesRepository : GenericRepository<MovieC>, IMoviesRepository
    {
        public MoviesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<MovieC>> GetByCategoryIdAsync(int id)
        {
            IQueryable<MovieC> movie = _dbContext.Movies.Where(p => p.CountryCategoryId == id);

            return movie.ToList();
        }
    }
}
