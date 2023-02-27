using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Movies.WebAdmin.ViewModels
{
    public class LoginViewModel
    {

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə alanı gereklidir.")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "şifreniz en 4 karakterli olmalıdır.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
