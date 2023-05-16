using Backend.Domain.Common;
using Backend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebApi.Middleware
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomClassAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IEnumerable<Enums.MembershipType> _roles;

        public CustomClassAuthorizeAttribute(params Enums.MembershipType[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var hasLocationHeader = context.HttpContext.Request.Headers.Any(x => x.Key == "Location");
            if (hasLocationHeader)
            {
                var locationHeader = context.HttpContext.Request.Headers["Location"];

                //Allow Swagger endpoints to execute actions without a valid key
                //if (locationHeader == "Swagger")
                //   return;
            }

            // authorization
            var validToken = context.HttpContext.Items["ValidTokenData"];
            if (validToken == null)
            {
                var errorResult = new ApiResponse<object>()
                {
                    Error = new List<object>() { new ApiExceptionResponse("Request ignored. Token required") },
                    StatusCode = 401,
                    Date = DateTime.Now
                };
                JsonSerializerOptions options = new() { WriteIndented = true, PropertyNamingPolicy = null };
                context.Result = new JsonResult(errorResult, options);
            }

            // policy applies to user type
            if (context.HttpContext!.Items["ValidTokenData"] is TokenData tokenData)
            {
                //var _ = Enum.TryParse(typeof(Enums.MembershipType), tokenData.MembershipType, out Enums.MembershipType membershipType);
                //var _ = Enum.TryParse(tokenData.MembershipType, out Enums.MembershipType membershipType);
                var membershipType = tokenData.MembershipType;
                //administrator can do anything
                if (membershipType == Enums.MembershipType.SysAdmin)
                {
                    return;
                }

                if (_roles.Contains(membershipType) == false)
                {
                    var errorResult = new ApiResponse<object>()
                    {
                        Error = new List<object>() { new ApiExceptionResponse("Request ignored. Resource permissions denied") },
                        StatusCode = 401,
                        Date = DateTime.Now
                    };
                    JsonSerializerOptions options = new() { WriteIndented = true, PropertyNamingPolicy = null };
                    context.Result = new ObjectResult(string.Empty)
                    {
                        StatusCode = 401,
                        Value = errorResult,
                    };
                }
            }

        }

    }
}
