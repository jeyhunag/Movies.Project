using AutoMapper;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<MovieC, MovieC>().ReverseMap();
            CreateMap<GenresCategory, GenresCategoryDto>().ReverseMap();
            //CreateMap<CountryCategory, CountryCategoryDto>().ReverseMap();
            CreateMap<LanguageCategory, LanguageCategoryDto>().ReverseMap();
            CreateMap<MoviesDocument, MoviesDocumentDto>().ReverseMap();
            CreateMap<About, AboutDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}
