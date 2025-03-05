using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserDbContext : IDbWithNav<User, Guid>
	{
        private readonly PetExchangeDbContext _dbcontext;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly string adminRole = "Administrator";
        public UserDbContext(PetExchangeDbContext petExchangeDbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _dbcontext = petExchangeDbContext;
            this.userManager = userManager;
            _signInManager = signInManager;
        }
        public UserDbContext(PetExchangeDbContext petExchangeDbContext)
        {
            _dbcontext = petExchangeDbContext;
        }
        public async Task CreateAsync(User entity, string passWord)
        {
            await userManager.CreateAsync(entity, passWord);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task ChangePassWord(User entity, string newPassWord)
        {
            var userFromDb = await userManager.FindByNameAsync(entity.UserName);
            var token = await userManager.GeneratePasswordResetTokenAsync(userFromDb);
            var result = await userManager.ResetPasswordAsync(userFromDb, token, newPassWord);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task CreateAsync(User entity)
        {
            try
            {
                await _dbcontext.Users.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<User> query = _dbcontext.Users;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Town).Include(e => e.Requests).Include(e => e.PublicOffers).Include(e => e.Pets);
                }
                return await query.SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<User> query = _dbcontext.Users;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.Town).Include(e => e.Requests).Include(e => e.PublicOffers).Include(e => e.Pets);
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(User user, bool useNavigationalProperties = true)
        {
            try
            {
                User userFromDb = await ReadAsync(user.Id, useNavigationalProperties, false);

                if (userFromDb is null)
                {
                    throw new ArgumentException("User with id = " + user.Id + "does not exist!");
                }
                if (useNavigationalProperties) _dbcontext.Users.Update(user); // updates all linked entities
                else
                {
                    _dbcontext.Users.Entry(userFromDb).CurrentValues.SetValues(user); // updates only the core entity
                }

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(Guid key)
        {
            try
            {
                User user = await ReadAsync(key, false, false);

                if (user is null)
                {
                    throw new ArgumentException("User with id = " + key + " does not exist!");
                }

                _dbcontext.Users.Remove(user);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
