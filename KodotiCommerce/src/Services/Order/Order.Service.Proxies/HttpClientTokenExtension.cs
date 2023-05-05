using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Order.Service.Proxies
{
    public static class HttpClientTokenExtension
    {
        public static void AddBearerToken(this HttpClient client, IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext.User.Identity.IsAuthenticated &&
                contextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = contextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                }
            }
        }
    }
}
