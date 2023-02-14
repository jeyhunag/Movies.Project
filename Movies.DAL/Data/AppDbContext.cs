using Microsoft.EntityFrameworkCore;
using Movies.DAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public  DbSet<MovieC> Movies { get; set; }
        public  DbSet<MoviesDocument> MoviesDocuments { get; set; }
    }
}
