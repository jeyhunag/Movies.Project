﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using Movies.DAL.Repostory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Services
{
    public class MoviesService : GenericService<MovieCDto, MovieC>, IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IGenericRepository<GenresCategory> _genresRepository;
        private readonly IGenericRepository<LanguageCategory> _languageRepository;
        private readonly IGenericRepository<CountryCategory> _countryRepository;
        private readonly IGenericRepository<MoviesDocument> _documentRepository;

        public MoviesService(IGenericRepository<MovieC> genericRepository,
            IMapper mapper, ILogger<GenericService<MovieCDto, MovieC>> logger,
            IGenericRepository<GenresCategory> genresRepository,
            IGenericRepository<LanguageCategory> languageRepository,
            IGenericRepository<CountryCategory> countryRepository,
            IMoviesRepository moviesRepository,
            IGenericRepository<MoviesDocument> documentRepository)
            : base(genericRepository, mapper, logger)
        {
            _genresRepository = genresRepository;
            _languageRepository = languageRepository;
            _countryRepository= countryRepository;
            _documentRepository= documentRepository;
            _moviesRepository= moviesRepository;
        }

        public async Task<List<CountryCategoryDto>> GetCountryCategoriesAsync()
        {
            var countryCategories = await _countryRepository.GetListAsync();

            var categoryDtos = _mapper.Map<List<CountryCategoryDto>>(countryCategories);
            return categoryDtos;
        }

        public async Task<List<GenresCategoryDto>> GetGenresCategoriesAsync()
        {
            var genresCategories = await _genresRepository.GetListAsync();

            var categoryDtos = _mapper.Map<List<GenresCategoryDto>>(genresCategories);
            return categoryDtos;
        }

        public async Task<List<LanguageCategoryDto>> GetLangaugeCategoriesAsync()
        {
            var languageCategories = await _languageRepository.GetListAsync();

            var categoryDtos = _mapper.Map<List<LanguageCategoryDto>>(languageCategories);

            return categoryDtos;
        }

        public async Task<List<MovieCDto>> GetMoviesByCategoryIdAsync(int id)
        {
            var movies = await _moviesRepository.GetByCategoryIdAsync(id);

            var moviesDtos = _mapper.Map<List<MovieCDto>>(movies);

            return moviesDtos;
        }

        public async Task<MovieCDto> GetMoviesDetailByIdAsync(int id)
        {
            var movies = await _moviesRepository.GetByIdAsync(id);
            List<MoviesDocument> documents = await _documentRepository.GetListAsync();

            movies.MoviesDocuments = documents.Where(d => d.MovieCId == id).ToList();
            var moviestDto = _mapper.Map<MovieCDto>(movies);

            return moviestDto;
        }
    }
}