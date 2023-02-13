using Microsoft.AspNetCore.Identity;

namespace UhanieWebApp.Data
{
    public class Customer:IdentityUser
    {
        public string LastName { get; set; }
        public DateTime RegisterOn { get; set; }
        public ICollection<OrderFlower> OrderFlowers { get; set; }
        public ICollection<OrderBouquet> OrderBouquets { get; set; }
    }
}
