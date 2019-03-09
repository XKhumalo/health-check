using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCheck.API.Services
{
    public static class CookieService
    {
        public static ActionResult AddCookie(this ActionResult result, HttpResponse response, string key, string value)
        {
            response.Cookies.Append(key, value, new CookieOptions()
            {
                Path = "/"
            });

            return result;
        }
    }
}
