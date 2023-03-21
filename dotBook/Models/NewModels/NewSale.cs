namespace dotBook.Models.NewModels
{
    public class NewSale
    {
        public int CustomerId { get; set; }
        public ICollection<SaleBook>? SaleBooks { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
