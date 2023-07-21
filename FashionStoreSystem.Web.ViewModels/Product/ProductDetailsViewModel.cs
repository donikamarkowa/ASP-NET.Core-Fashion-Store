﻿using FashionStoreSystem.Web.ViewModels.Seller;

namespace FashionStoreSystem.Web.ViewModels.Product
{
    public class ProductDetailsViewModel : ProductAllViewModel
    {
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;

        public SellerInfoOnHouseViewModel Seller { get; set; } = null!; 
    }
}
