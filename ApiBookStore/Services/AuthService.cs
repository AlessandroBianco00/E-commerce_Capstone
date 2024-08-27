using ApiBookStore.Context;
using ApiBookStore.Interfaces;
using ApiBookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBookStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly DataContext _ctx;

        public AuthService(IPasswordEncoder passwordEncoder, IConfiguration config, DataContext dataContext) : base()
        {
            _passwordEncoder = passwordEncoder;
            _ctx = dataContext;
        }
        public async Task<User> CreateUser(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _ctx.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && _passwordEncoder.IsSame(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public async Task<User> Register(User user)
        {
            var u =
            new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = _passwordEncoder.Encode(user.Password),
            };
            var userRole = _ctx.Roles.FirstOrDefault(r => r.RoleName == "User");
            if (userRole != null)
            {
                u.Roles.Add(userRole);
            }

            return await CreateUser(u);
        }
    }
}

