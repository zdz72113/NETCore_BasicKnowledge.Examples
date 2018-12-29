using System.Security.Claims;
using System.Threading.Tasks;
using SignalRJWTDemo.Test;

namespace SignalRJWTDemo.Authentication
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, string refreshToken, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(User user);
    }
}
