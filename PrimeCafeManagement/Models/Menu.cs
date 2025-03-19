namespace PrimeCafeManagement.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime CreatedOn { get; set;}
        public string AccessToken { get; set; }
        public virtual MenuTitle MenuTitle { get; set; }
        public int MenuTitleId { get; set; }
        public virtual MenuPrice MenuPrice { get; set; }
        public int MenuPriceId { get; set; }


    }
}
