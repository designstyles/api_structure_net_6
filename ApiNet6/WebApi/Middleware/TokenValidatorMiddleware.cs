using Backend.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Middleware
{
    public class TokenValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenValidatorService tokenValidator)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers.TryGetValue("Bearer", out var tokenHeaderValues);

            if (token)
            {
                var tokenResult = await tokenValidator.ValidateToken(tokenHeaderValues);
                if (tokenResult != null)
                {
                    context.Items["ValidTokenData"] = tokenResult;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, tokenResult.ProfileId.ToString()),
                        new Claim(ClaimTypes.Role, tokenResult.MembershipType.ToString()),
                        new Claim(ClaimTypes.UserData, tokenResult.MembershipType.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, "API Key");
                    var identities = new List<ClaimsIdentity> { identity };
                    var principal = new ClaimsPrincipal(identities);
                    context.User = principal;
                }
            }
            await _next(context);
        }
    }

}
