﻿using ApiBookStore.Context;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using ApiBookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiBookStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly DataContext _context;
        private readonly string issuer;
        private readonly string audience;
        private readonly string key;

        public AuthService(IPasswordEncoder passwordEncoder, IConfiguration config, DataContext dataContext) : base()
        {
            _passwordEncoder = passwordEncoder;
            _context = dataContext;
            // apsettings.json data
            issuer = config["Jwt:Issuer"]!;
            audience = config["Jwt:Audience"]!;
            key = config["Jwt:Key"]!;
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.Carts.Add(new Cart { UserId = user.UserId });
            _context.Wishlists.Add(new Wishlist { UserId = user.UserId });
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Register(User user)
        {
            var u =
            new User
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = _passwordEncoder.Encode(user.Password),
                DeletedAt = null
            };
            var userRole = _context.Roles.FirstOrDefault(r => r.RoleName == "User"); // Necessario che sia presente il Role "User" nel DB affinchè sia assegnato
            if (userRole != null)
            {
                u.Roles.Add(userRole);
            }

            return await CreateUser(u);
        }

        public async Task<User> Login(LoginModel model)
        {
            var encodedPassword = _passwordEncoder.Encode(model.Password);
            var user = await _context.Users
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == model.Email && encodedPassword == u.Password);

            if (user != null )
            {
                return user;
            }

            return null;
        }

        //inutilizzato (sincrono)
        public async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim("UserId", user.UserId.ToString())
            };
            user.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.RoleName)));

            var k = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            var signed = new SigningCredentials(k, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddMonths(1);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: signed
            );

            return token;
        }
    }
}

