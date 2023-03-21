namespace dotBook.NewModels
{
    public class NewSale
    {
        public int CustomerId { get; set; }
        public ICollection<NewSaleBook>? NewSaleBook { get; set; }
        //public decimal TotalPrice { get; set; }
    }
}
