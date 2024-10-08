﻿using ApiBookStore.Entities;
using ApiBookStore.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ApiBookStore.Interfaces
{
    public interface IAuthService
    {
        public Task<User> Login(LoginModel model);
        public Task<JwtSecurityToken> GenerateToken(User user);
        public Task<User> CreateUser(User user);
        public Task<User> Register(User user);
    }
}
