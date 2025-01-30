using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models {
    public class Product {
        [Display(Name = "Ürün Id")]
        public int ProductId { get; set; }
        [Display(Name = "Ürün Adı")]
        [StringLength(maximumLength: 20, ErrorMessage = "6-10 karakter arası olmalu", MinimumLength = 1)]
        [Required(ErrorMessage = "Bu alan boş olamaz")]
        public string Name { get; set; } = null!; // null forgiveness
        [Display(Name = "Fiyat")]
        [Required]
        [Range(0, 100000, ErrorMessage = "Değer 0 ile 100000 arasında olmalıdır.")]
        public decimal? Price { get; set; }

        [Display(Name = "Resim")]
        public string? Image { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Display(Name = "Category")]
        [Required]
        public int? CategoryId { get; set; }
    }
}