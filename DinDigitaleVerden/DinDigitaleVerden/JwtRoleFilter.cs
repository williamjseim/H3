using DinDigitaleVerden.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DinDigitaleVerden.Data;
using Microsoft.EntityFrameworkCore;

namespace DinDigitaleVerden
{
    public class JwtRoleFilter : Attribute, IAuthorizationFilter
    {
        private Roles allowedRole;
        public JwtRoleFilter(Roles role)
        {
            this.allowedRole = role;
        }
        /// <summary>
        /// checks if the user has the correct role
        /// </summary>
        /// <param name="context"></param>
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                VerdenDbContext db = context.HttpContext.RequestServices.GetService<VerdenDbContext>();
                IConfiguration config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                if (context.HttpContext.Request.Cookies.TryGetValue(config["Jwt:CookieName"], out string? token))
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    TokenValidationParameters parameters = new TokenValidationParameters
                    {
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidateActor = false,
                    };
                    var result = handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                    Guid userId = Guid.Parse(result.Claims.Where(I => I.Type == "Id").First().Value);
                    UserModel user = await db.User_Data.Where(i => i.Id == userId).FirstOrDefaultAsync();
                    if(user != null)
                    {
                        if (user.Role >= this.allowedRole)
                        {
                            context.Result = new StatusCodeResult(StatusCodes.Status200OK);
                            return;
                        }
                    }
                }
                context.Result = new UnauthorizedResult();
                return;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
