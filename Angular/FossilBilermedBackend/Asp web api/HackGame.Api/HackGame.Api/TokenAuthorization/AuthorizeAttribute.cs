using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HackGame.Api.TokenAuthorization
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                try
                {
                    IConfiguration config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                    string token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                    Console.WriteLine(token + " dawdasdighoasijhfoiuashoifah");
                    token = token.Replace("Bearer ", string.Empty);
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    TokenValidationParameters parameters = new TokenValidationParameters
                    {
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateActor = false,
                    };
                    var result = handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                    return;
                }
                catch
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
    }

    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        string role;
        public RoleAuthorizeAttribute(string role)
        {
            this.role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                IConfiguration config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                string token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                Console.WriteLine(token + " dawdasdighoasijhfoiuashoifah");
                token = token.Replace("Bearer ", string.Empty);
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters parameters = new TokenValidationParameters
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateActor = false,
                };
                var result = handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                var role = result.Claims.Where(i => i.Type == "Role").FirstOrDefault();
                if(role == null || role.Value != this.role)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                return;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
