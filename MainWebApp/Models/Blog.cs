using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MainWebApp.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(450, MinimumLength = 10, ErrorMessage = "Min 10 herfden ibaret olmalidir")]

        public string Name { get; set; }
        [Required]
        public string Context { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [NotMapped]
        [Required]
        public IFormFile File { get; set; }

    }
}
