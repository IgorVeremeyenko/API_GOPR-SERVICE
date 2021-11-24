using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_GOPR_SERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCockie : ControllerBase
    {
       /* // POST: /sessionLogin
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            // Set session expiration to 14 days.
            var options = new SessionCookieOptions()
            {
                ExpiresIn = TimeSpan.FromDays(14),
            };

            try
            {
                // Create the session cookie. This will also verify the ID token in the process.
                // The session cookie will have the same claims as the ID token.
                var sessionCookie = await FirebaseAuth.DefaultInstance
                    .CreateSessionCookieAsync(request.IdToken, options);

                // Set cookie policy parameters as required.
                var cookieOptions = new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.Add(options.ExpiresIn),
                    HttpOnly = true,
                    Secure = true,
                };
                this.Response.Cookies.Append("session", sessionCookie, cookieOptions);
                return this.Ok();
            }
            catch (FirebaseAuthException)
            {
                return this.Unauthorized("Failed to create a session cookie");
            }
        }
*/
    }
}
