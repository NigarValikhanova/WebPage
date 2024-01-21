using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Min 3 herfden ibaret olmalidir")]
        public string Name { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public virtual Category Category { get; set; }
        [NotMapped]
        [Required]
        public IFormFile File { get; set; }
        public string TrendRate { get; set; }
    }
}
