﻿using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FashionStoreSystem.Services.Data
{
    public class UserService : IUserService
    {
        private readonly FashionStoreDbContext dbContext;

        public UserService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }
        public async Task AddMoneyToWallet(string userId, decimal money)
        {
            var user = await this.dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            user.Wallet += money;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetWalletBalanceByUserIdAsync(string userId)
        {
            var userWallet = await this.dbContext
                .Users
                .Where(u => u.Id.ToString() == userId)
                .Select(u => u.Wallet)
                .FirstOrDefaultAsync();

            return userWallet;
        }

        public async Task<bool> UserExistsByIdAsync(string userId)
        { 
            bool result = await this.dbContext
                .Users
                .AnyAsync(u => u.Id.ToString() == userId);

            return result;
        }

    }
}