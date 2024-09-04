using ApiBookStore.Context;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using ApiBookStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        private readonly DataContext _ctx;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly IAuthService _authService;
        private readonly string issuer;
        private readonly string audience;
        private readonly string key;

        public AuthenticationController(DataContext dataContext, IConfiguration config, IPasswordEncoder passwordEncoder, IAuthService authService)
        {
            _ctx = dataContext;
            _passwordEncoder = passwordEncoder;
            _authService = authService;
            // apsettings.json data
            issuer = config["Jwt:Issuer"]!;
            audience = config["Jwt:Audience"]!;
            key = config["Jwt:Key"]!;
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await _authService.Login(model);

            if (user == null) return Unauthorized();

            var claims = new List<Claim> { // Claim da inserire nel token
                new Claim(JwtRegisteredClaimNames.Name, model.Email),
                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                // oltre a quelle standard ne metto una che uso nei miei servizi
                new Claim("UserId", user.UserId.ToString())
            };
            user.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.RoleName)));

            // chiave per la firma
            var k = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            // applicazione della chiave per la firma
            var signed = new SigningCredentials(k, SecurityAlgorithms.HmacSha256);
            // data di scadenza del token
            var expiration = DateTime.Now.AddMonths(1);
            // creazione del token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims, // claim veicolati tramite
                expires: expiration,
                signingCredentials: signed
            );

            return Ok(new LoginResponseModel
            { // risposta di login a buon fine
                // questo è il token da restituire al client
                Token = new JwtSecurityTokenHandler().WriteToken(token), // writetoken lo scrive come stringa
                TokenExpiration = expiration,
                User = new User
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Roles = user.Roles.Select(role => new Role
                    {
                        RoleId = role.RoleId,
                        RoleName = role.RoleName
                    }).ToList()
                }
            });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            await _authService.Register(model);
            return Ok(new { Message = "Registration successful", Email = model.Email });
        }
    }
}
