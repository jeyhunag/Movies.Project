using Movies.DAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repostory.Interfaces
{
    public interface IMoviesRepository: IGenericRepository<MovieC>
    {
        public Task<List<MovieC>> GetByCategoryIdAsync(int id);
    }
}
