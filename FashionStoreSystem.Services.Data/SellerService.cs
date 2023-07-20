﻿using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Seller;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class SellerService : ISellerService
    {
        private readonly FashionStoreDbContext dbContext;
        public SellerService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(string userId, BecomeSellerFormModel model)
        {
            Seller newSeller = new Seller()
            {
                UserId = Guid.Parse(userId),
                PhoneNumber = model.PhoneNumber,
            };

            await this.dbContext.Sellers.AddAsync(newSeller);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                .Sellers
                .AnyAsync(s => s.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> SellerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Sellers
                .AnyAsync(s => s.UserId.ToString() == userId);

            return result;
        }

        public async Task<bool> SelllerHasProductsByUserIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            return user.Products.Any(); 
        }
    }
}
