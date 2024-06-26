﻿using BoilerplateCleanArch.Domain.Entities;
using BoilerplateCleanArch.Domain.Interfaces.IUserRepository;
using BoilerplateCleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BoilerplateCleanArch.Infra.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _userContext;
        public UserRepository(ApplicationDbContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            _userContext.Add(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByIdAsync(int? id)
        {
            return await _userContext.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userContext.Users.ToListAsync();
        }

        public async Task<User> RemoveAsync(User user)
        {
            _userContext.Remove(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _userContext.Update(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async void SaveToken(User user, string token, DateTime? expiration)
        {
            var userDb = await GetByIdAsync(user.Id);
            userDb.AccessToken = token;
            userDb.Expiration = expiration;

            await UpdateAsync(userDb);
        }
    }
}
