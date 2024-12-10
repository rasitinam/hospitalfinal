using System.ComponentModel.DataAnnotations;

namespace hospitalfinal.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Tipi gereklidir.")]
        public string UserType { get; set; }
        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }
    }
}
