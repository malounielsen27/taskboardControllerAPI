
using backend.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
      
        public async Task<int> Register(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id; 
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
