using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{
    public class About: BaseEntity
    {
        public string Description { get; set; }
        public string? Img { get; set; }
        public string Position { get; set; }
    }
}
