using Movies.DAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Dtos
{
    public class MovieCDto:BaseDto
    {
        public string Name { get; set; }
        public byte Age { get; set; }
        public TimeSpan MovieTime { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string CountryCategoryId { get; set; }
        public string GenresCategoryId { get; set; }
        public string LanguageCategoryId { get; set; }
        public string CountryCategoryName { get; set; }
        public string LanguageCategoryName { get; set; }
        public string GenresCategoryName { get; set; }
        public string TrandsId { get; set; }
        public string Director { get; set; }
        public string? Img { get; set; }
        public string? MovieVideo { get; set; }
        public string? Trailer { get; set; }
        public bool Top { get; set; }
        public bool isneww { get; set; }


        public List<MoviesDocumentDto> MoviesDocumentDtos { get; set; }

        public List<CountryCategoryDto> CountryCategoryDtos { get; set; }
        public List<TrandCategoryDto> TrandCategoryDtos { get; set; }
        public List<GenresCategoryDto> GenresCategoryDtos { get; set; }
        public List<LanguageCategoryDto> LanguageCategoryDtos { get; set; }
    }
}
