﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace DataLayer
{
    public class UserDbContext : IDbWithNav<User, Guid>
    {
        private readonly PetExchangeDbContext _dbcontext;
        private readonly UserManager<User> userManager;
        private readonly RoleEnum adminRole = RoleEnum.Admin;
        private readonly RoleEnum userRole = RoleEnum.User;

        public UserDbContext(PetExchangeDbContext petExchangeDbContext, UserManager<User> userManager)
        {
            _dbcontext = petExchangeDbContext;
            this.userManager = userManager;
        }

        public UserDbContext(PetExchangeDbContext petExchangeDbContext)
        {
            _dbcontext = petExchangeDbContext;
        }

        public async Task ChangePassWord(User entity, string newPassWord)
        {
            var userFromDb = await userManager.FindByNameAsync(entity.UserName);
            var token = await userManager.GeneratePasswordResetTokenAsync(userFromDb);
            var result = await userManager.ResetPasswordAsync(userFromDb, token, newPassWord);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<User>> ReadAllWithFilterAsync(string username, string name, 
            string email, string town, string role, int page = 1, 
            int pageSize = 10, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            var allUsers = await ReadAllAsync(useNavigationalProperties, isReadOnly);
            // филтриране
            var filteredUsers = allUsers.Where(x =>
                    (String.IsNullOrWhiteSpace(username) || x.UserName.Contains(username))
                    && (String.IsNullOrWhiteSpace(name) || x.Name.Contains(name))
                    && (String.IsNullOrWhiteSpace(email) || x.Email.Contains(email))
                    && (String.IsNullOrWhiteSpace(town) || x.Town.Name.ToLower().Contains(town.ToLower()))
                    && (String.IsNullOrWhiteSpace(role) || x.Role.ToDescriptionString().ToLower().Contains(role.ToLower()))
                    ).ToList();
            // странициране
            filteredUsers = filteredUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return filteredUsers;
        }

        #region CRUD
        public async Task CreateAsync(User entity, string passWord)
        {
            await userManager.CreateAsync(entity, passWord);
            if (entity.Role == adminRole)
            {
                await userManager.AddToRoleAsync(entity, adminRole.ToString());
            }
            else if (entity.Role == userRole)
            {
                await userManager.AddToRoleAsync(entity, userRole.ToString());
            }
            await _dbcontext.SaveChangesAsync();
        }

        public async Task CreateAsync(User entity)
        {
            await _dbcontext.Users.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            if (entity.Role == adminRole)
            {
                await userManager.AddToRoleAsync(entity, adminRole.ToString());
            }
            else if (entity.Role == userRole)
            {
                await userManager.AddToRoleAsync(entity, userRole.ToString());
            }
        }

        public async Task CreateAsync(List<User> users)
        {
            foreach (var user in users)
            {
                await CreateAsync(user);
            }
        }

        public async Task<User>? ReadAsync(Guid id, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            IQueryable<User> query = _dbcontext.Users;
            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            if (useNavigationalProperties)
            {
                query = query.Include(e => e.Town).Include(e => e.RequestOutbox).Include(e => e.Pets);
            }
            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<User>> ReadAllAsync(bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            IQueryable<User> query = _dbcontext.Users;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            if (useNavigationalProperties)
            {
                query = query.Include(e => e.Town).Include(e => e.RequestOutbox).Include(e => e.Pets);
            }
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(User user, bool useNavigationalProperties = true)
        {
            User userFromDb = await ReadAsync(user.Id, useNavigationalProperties, false);

            if (userFromDb is null)
            {
                throw new ArgumentException("User with id = " + user.Id + "does not exist!");
            }
            if (useNavigationalProperties) _dbcontext.Users.Update(user); // Актуализира всички навигационни свойства
            else
            {
                _dbcontext.Users.Entry(userFromDb).CurrentValues.SetValues(user); // Актуализира само текущият обект
            }

            if (user.Role == adminRole && user.Role != userFromDb.Role)
            {
                await userManager.RemoveFromRoleAsync(user, userRole.ToString());
                await userManager.AddToRoleAsync(user, adminRole.ToString());
            }
            else if (user.Role == userRole && user.Role != userFromDb.Role)
            {
                await userManager.RemoveFromRoleAsync(user, adminRole.ToString());
                await userManager.AddToRoleAsync(user, userRole.ToString());
            }
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid key)
        {
            User user = await ReadAsync(key, false, false);

            if (user is null)
            {
                throw new ArgumentException("User with id = " + key + " does not exist!");
            }
            await userManager.DeleteAsync(user);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion
    }
}
