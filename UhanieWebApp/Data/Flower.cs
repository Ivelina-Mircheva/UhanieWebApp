namespace UhanieWebApp.Data
{
    public class Flower
    {
        public int Id { get; set; }
        public string BulgarianName { get; set; }
        public string LatinName { get; set; }
        public int Size { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<OrderFlower> Orders { get; set; }
    }
}
