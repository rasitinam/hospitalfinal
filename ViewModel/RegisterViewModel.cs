
using System.ComponentModel.DataAnnotations;

namespace hospitalfinal.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Tipi gereklidir.")]
        public string UserType { get; set; }

        [Required(ErrorMessage = "Ad gereklidir.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad gereklidir.")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "T.C. Kimlik No gereklidir.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "T.C. Kimlik No 11 haneli olmalıdır.")]
        public string TC { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon gereklidir.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre Tekrar gereklidir.")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Adres gereklidir.")]
        public string Adress { get; set; }
    }
}
