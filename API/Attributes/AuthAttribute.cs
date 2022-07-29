using Api.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Api.Entities.Exceptions;
using Api.Shared;

namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as User;
            if (user == null)
                throw new UnauthorizedException();

            var userId = context.HttpContext.Request.Query.FirstOrDefault(x => x.Key == "userId");
            if (!string.IsNullOrEmpty(userId.Value) && user.Id != userId.Value)
                throw new UnauthorizedException();
        }
    }
}
