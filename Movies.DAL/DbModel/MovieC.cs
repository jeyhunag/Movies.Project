using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Movies.DAL.DbModel
{
    public class MovieC:BaseEntity
    {
        public string Name { get; set; }
        public byte Age { get; set; }
        public TimeSpan MovieTime { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string? Img { get; set; }
        public string? MovieVideo { get; set; }
        public string? Trailer { get; set; }

        public virtual ICollection<MoviesDocument>? MoviesDocuments { get; set; }
    }
}
