using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{
    public class CountryCategory:BaseEntity
    {
   
        public string Name { get; set; }
        public virtual ICollection<MovieC> Movies { get; set; }
    }
}
