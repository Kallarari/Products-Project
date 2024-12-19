using System.ComponentModel.DataAnnotations;

namespace dotnet_API.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string CNPJ { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty; 

        public ICollection<Product> Products { get; set; }
    }
}
