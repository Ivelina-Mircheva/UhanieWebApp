namespace UhanieWebApp.Data
{
    public class OrderBouquet
    {
        public int Id { get; set; }
        public int BouquetId { get; set; }
        public Bouquet Bouquets { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Quantity { get; set; }
        public DateTime RegisterOn { get; set; }
    }
}
