

using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
