using ApiBookStore.Context;
using ApiBookStore.Models;
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
        
        private readonly DataContext dataContext;
        private readonly string issuer;
        private readonly string audience;
        private readonly string key;

        public AuthenticationController(DataContext dataContext, IConfiguration config)
        {
            this.dataContext = dataContext;
            // apsettings.json data
            issuer = config["Jwt:Issuer"]!;
            audience = config["Jwt:Audience"]!;
            key = config["Jwt:Key"]!;
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // recupera l'utente dal database.
            var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            // se non viene recuperato l'utente, rilascia un codice Unauthorized
            if (user == null) return Unauthorized();
            var claims = new[] { // Claim da inserire nel token
                new Claim(JwtRegisteredClaimNames.Name, model.Email),
                new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                // oltre a quelle standard ne metto una che uso nei miei servizi
                new Claim("UserId", user.UserId.ToString())
            };
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
                Email = user.Email,
                TokenExpiration = expiration,
                UserId = user.UserId,
            });
        }
    }
}
