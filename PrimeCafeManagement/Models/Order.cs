
namespace PrimeCafeManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Menu Menu { get; set; }
        public int MenuId { get; set; }
    }
}
