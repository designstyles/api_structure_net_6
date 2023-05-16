using Backend.Domain.Common;
using Backend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebApi.Middleware
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomMethodAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IEnumerable<Enums.MembershipType> _roles;

        public CustomMethodAuthorizeAttribute(params Enums.MembershipType[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;


            // policy applies to user type
            if (context.HttpContext!.Items["ValidTokenData"] is TokenData tokenData)
            {
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
                        Error = new List<object>() { new ApiExceptionResponse("Request ignored. Membership type denied Resource request") },
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
