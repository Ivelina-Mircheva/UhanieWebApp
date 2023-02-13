namespace UhanieWebApp.Data
{
    public class OrderFlower
    {
        public int Id { get; set; }
        public int FlowerId { get; set; }
        public Flower Flower { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int Quantity { get; set; }
        public DateTime RegisterOn { get; set; }
    }
}
