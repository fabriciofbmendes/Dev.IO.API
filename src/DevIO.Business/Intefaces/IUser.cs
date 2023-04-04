using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
