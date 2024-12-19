using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    };
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
}
