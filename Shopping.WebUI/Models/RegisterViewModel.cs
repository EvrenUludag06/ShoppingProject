using System.ComponentModel.DataAnnotations;

namespace Shopping.WebUI.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Ad")]
        [MaxLength(25)]
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(25)]
        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        public string LastName { get; set; }

        [Display(Name = "Eposta")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Eposta alanı boş bırakılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")]
        public string PasswordConfirm { get; set; }
    }
}
