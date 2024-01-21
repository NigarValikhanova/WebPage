using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MainWebApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public int Test { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
