using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductAPI.Models;

namespace ProductAPI.DTO
{
    public class CategoryDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public virtual List<Product> Products { get; set; }
    }
}
