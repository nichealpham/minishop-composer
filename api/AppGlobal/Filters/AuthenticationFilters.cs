using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AppGlobal.Services;
using AppGlobal.Models;
using AppGlobal.Extensions;
using AppGlobal.Commons;
using System.Threading.Tasks;
using AppGlobal.Entities;
using AppGlobal.Constants;
using AppGlobal.DBContext;
using System.Collections.Generic;

namespace AppGlobal.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = (AuthEntity)context.HttpContext.Items["Auth"];
            if (auth == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }

    public class ClinicOwnerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = (AuthEntity)context.HttpContext.Items["Auth"];

            context.HttpContext.Request.RouteValues.TryGetValue("clinicID", out object clinicID);

            if (!auth.Roles.Any(role => role.RoleType == (int)RoleType.Owner && role.ClinicID.ToLower() == clinicID.ToString().ToLower()))
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }

    public class ClinicStaffAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var staffRoles = new List<byte?>() { (int)RoleType.Doctor, (int)RoleType.Receptionist, (int)RoleType.Owner };

            var auth = (AuthEntity)context.HttpContext.Items["Auth"];

            context.HttpContext.Request.RouteValues.TryGetValue("clinicID", out object clinicID);

            if (!auth.Roles.Any(role => staffRoles.Contains(role.RoleType) && role.ClinicID.ToLower() == clinicID.ToString().ToLower()))
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, AccessTokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, tokenService, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, AccessTokenService tokenService, string token)
        {
            try
            {
                var auth = tokenService.ParseJwtToken(token);
                context.Items["Token"] = token;
                context.Items["Auth"] = auth;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
