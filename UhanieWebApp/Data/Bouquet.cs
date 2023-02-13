namespace UhanieWebApp.Data
{
    public enum BouquetType { bouquet, arrangement }
    public class Bouquet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BouquetType Type { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<OrderBouquet> Orders { get; set; }
    }
}
