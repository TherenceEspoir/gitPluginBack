/*
namespace GitPlugin.Controllers;

public class IdentityController
{
    
}

*/


using GitPlugin.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GitPlugin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    [HttpPost("token")]
    public IActionResult GenerateToken([FromBody] IdentityRequest request)
    {
        try
        {
            if ((request.UserEmail != "betco@betco.com" && request.Password != "toto") && (request.UserEmail != "clement@betco.com" && request.Password != "tata"))
            {
                throw new UnauthorizedAccessException("username or password are not valid");
            }

            string userRole = GetUserRole(request.UserEmail);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("lecoqchantelejoursarretetoutselevedanslevillage");
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.UserEmail),
            new(JwtRegisteredClaimNames.Email, request.UserEmail),
            new("userRole", userRole)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                Issuer = "http://localhost:5299",
                Audience = "http://localhost:5299",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(securityToken);

            return Ok(jwt);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    private static string GetUserRole(string userEmail) => userEmail == "betco@betco.com" ? "admin" : "reader";
}