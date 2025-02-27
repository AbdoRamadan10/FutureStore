using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace FutureStore.Authentication.BasicAuthntication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());


            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"],out var authenticationHeader))
                return Task.FromResult(AuthenticateResult.Fail("Unknown scheme"));

            //string authorizationHeader = Request.Headers["Authorization"].ToString();
            //if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            //    return Task.FromResult(AuthenticateResult.Fail("Unknown scheme"));
            //var encodedCredentials = authorizationHeader["Basic ".Length..]; // Extract the base64 encoded username and password


            var encodedCredentials = authenticationHeader.Parameter;
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var userNameAndPassword = decodedCredentials.Split(':');

            if (userNameAndPassword[0] != "admin" || userNameAndPassword[1] != "123")
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));

            var idetity = new ClaimsIdentity(new Claim[] {

                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, userNameAndPassword[0]),
                new Claim(ClaimTypes.Role, "Manager")
                //You can add more claims if needed like roles etc.

            }, "Basic");

            var principal = new ClaimsPrincipal(idetity);

            var ticket = new AuthenticationTicket(principal, "Basic");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
