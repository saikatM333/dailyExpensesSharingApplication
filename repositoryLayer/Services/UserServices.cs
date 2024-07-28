using repositoryLayer.context;
using repositoryLayer.entity;
using repositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace repositoryLayer.Services
{
    public class UserService : IUserService
    {
        private readonly expensesDBContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(expensesDBContext context , ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(User user)
        {   if (user == null) throw new ArgumentNullException(nameof(user));
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                throw new ApplicationException("An error occurred while creating the user.");
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
