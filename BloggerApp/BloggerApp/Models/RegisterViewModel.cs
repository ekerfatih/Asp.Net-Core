using System.ComponentModel.DataAnnotations;

namespace BloggerApp.Models {
    public class RegisterViewModel() {

        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string? Name { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "E-posta Adresi")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter olabilir", MinimumLength = 3)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola Tekrar")]
        [Compare(nameof(Password),ErrorMessage = "Parolanız Eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }

    }
}