using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected const string AuthenticationType = "Token";

        protected Guid UserId
        {
            get
            {
                var value = User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
                if (value is not null)
                    return Guid.Parse(value);

                throw new Exception("Undefined user");
            }
        }
    }
}
