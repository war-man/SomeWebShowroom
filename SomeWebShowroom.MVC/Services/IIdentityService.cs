using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Services
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string role, string secret);
    }
}
