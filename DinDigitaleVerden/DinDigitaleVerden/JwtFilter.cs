using DinDigitaleVerden.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;

namespace DinDigitaleVerden
{
    public class JwtFilter : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// checks if the user has a jwt token with the correct key
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
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
                    context.Result = new StatusCodeResult(StatusCodes.Status200OK);
                    return;
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
