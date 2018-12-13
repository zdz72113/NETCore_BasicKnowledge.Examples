using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityDemo.Authentication.JWT.AuthHelper
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, string refreshToken, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(User user);
    }
}
