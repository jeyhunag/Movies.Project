using Movie.WEBUI.wwwroot.ViewModels;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movie.WEBUI.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Trends>? Trends { get; set; }
        public IEnumerable<GenresCategory>? GenresCategories { get; set; }
        public SignInViewModel SignInViewModel { get; set; }
        public MovieC MovieC { get; set; }
        public SignUpViewModel? SignUpViewModel { get; set; }
        public PagedViewModel<MovieC> PagedViewModel { get; set; }
        public IEnumerable<AboutDto>? Abouts { get; set; }

    }
}
