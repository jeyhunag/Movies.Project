using Movie.WEBUI.wwwroot.ViewModels;
using Movies.DAL.DbModel;

namespace Movie.WEBUI.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Trends>? Trends { get; set; }
        public IEnumerable<GenresCategory>? GenresCategories { get; set; }
        public SignInViewModel SignInViewModel { get; set; }
        public SignUpViewModel? SignUpViewModel { get; set; }
    }
}
