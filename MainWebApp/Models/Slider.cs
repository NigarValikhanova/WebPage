using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainWebApp.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CollectionName { get; set; }
        public string Title {  get; set; }
        [ValidateNever]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
