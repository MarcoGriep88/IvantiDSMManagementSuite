using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packless.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string AccessLevel { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
        }
    }
}
