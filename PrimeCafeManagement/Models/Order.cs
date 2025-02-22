namespace PrimeCafeManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }

    }
}
