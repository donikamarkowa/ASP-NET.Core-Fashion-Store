namespace FashionStoreSystem.Web.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string SellerFullName { get; set; } = null!;
        public string SellerEmail { get; set; } = null!;
        public string BuyerFullName { get; set; } = null!;
        public string BuyerEmail { get; set; } = null!;
    }
}
