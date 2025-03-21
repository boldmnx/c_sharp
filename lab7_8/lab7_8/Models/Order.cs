namespace lab7_8.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Address ShippingAddress { get; set; }
    }
}
