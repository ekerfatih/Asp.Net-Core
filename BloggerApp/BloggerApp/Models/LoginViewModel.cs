using System.ComponentModel.DataAnnotations;

namespace BloggerApp.Models {
    public class LoginViewModel() {
        [Required]
        [EmailAddress]
        [Display(Name = "E-posta Adresi")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter olabilir", MinimumLength = 3)] 
        [Display(Name ="Parola")]
        public string? Password { get; set; }
    }
}