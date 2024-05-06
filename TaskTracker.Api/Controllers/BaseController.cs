﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected const string OtpHeader = "X-OTP";
        protected const string DeviceIdHeader = "X-Device-Id";

        protected string UserId
        {
            get
            {
                var value = User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
                if (value is not null) return value;
                throw new Exception("Undefined user");
            }
        }
    }
}
