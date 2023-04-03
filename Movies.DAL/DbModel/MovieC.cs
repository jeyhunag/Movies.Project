using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// The Movies.DAL.DbModel namespace contains the data access layer classes related to movies.
/// </summary>
namespace Movies.DAL.DbModel
{
    /// <summary>
    /// The MovieC class represents a movie and its associated details.
    /// Inherits from BaseEntity.
    /// </summary>
    public class MovieC : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the movie.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age rating of the movie.
        /// </summary>
        public byte Age { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie.
        /// </summary>
        public TimeSpan MovieTime { get; set; }

        /// <summary>
        /// Gets or sets the release year of the movie.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the description of the movie.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the director of the movie.
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// Gets or sets the optional image URL for the movie.
        /// </summary>
        public string? Img { get; set; }

        /// <summary>
        /// Gets or sets the optional movie video URL.
        /// </summary>
        public string? MovieVideo { get; set; }

        /// <summary>
        /// Gets or sets the optional trailer URL for the movie.
        /// </summary>
        public string? Trailer { get; set; }

        /// <summary>
        /// Gets or sets the genre category ID for the movie.
        /// </summary>
        public int GenresCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the optional associated genre category for the movie.
        /// </summary>
        public GenresCategory? GenresCategory { get; set; }

        /// <summary>
        /// Gets or sets the country category ID for the movie.
        /// </summary>
        public int CountryCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the optional associated country category for the movie.
        /// </summary>
        public CountryCategory? CountryCategory { get; set; }

        /// <summary>
        /// Gets or sets the language category ID for the movie.
        /// </summary>
        public int LanguageCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the optional associated language category for the movie.
        /// </summary>
        public LanguageCategory? LanguageCategory { get; set; }

        /// <summary>
        /// Gets or sets the optional trend ID for the movie.
        /// </summary>
        public int? TrendId { get; set; }

        /// <summary>
        /// Gets or sets the optional associated trend for the movie.
        /// </summary>
        public Trends? Trend { get; set; }

        /// <summary>
        /// Gets or sets the optional collection of movie documents associated with the movie.
        /// </summary>
        public virtual ICollection<MoviesDocument>? MoviesDocuments { get; set; }

        /// <summary>
        /// Gets or sets whether the movie is a top-rated movie.
        /// </summary>
        public bool Top { get; set; }

        /// <summary>
        /// Gets or sets whether the movie is new.
        /// </summary>
        public bool isneww { get; set; }
    }
}
