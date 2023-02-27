using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace UhanieWebApp.Data
{
    public enum BouquetType { букет, аранжировка }
    public class Bouquet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BouquetType Type { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<OrderBouquet> Orders { get; set; }
    }
}
