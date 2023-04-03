using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Movies.DAL.DbModel namespace contains the data access layer classes related to movies.
/// </summary>
namespace Movies.DAL.DbModel
{
    /// <summary>
    /// The LanguageCategory class represents a category of languages for movies.
    /// Inherits from BaseEntity.
    /// </summary>
    public class LanguageCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the language category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of movies associated with the language category.
        /// </summary>
        public virtual ICollection<MovieC> Movies { get; set; }
    }
}

