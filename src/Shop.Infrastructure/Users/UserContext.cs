using Microsoft.AspNetCore.Http;
using Shop.Application.Users;
using System.Security.Claims;

namespace Shop.Infrastructure.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        ClaimsPrincipal? user = httpContextAccessor?.HttpContext?.User;

        if (user is null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            throw new UnauthorizedAccessException();

        var email = user.FindFirstValue(ClaimTypes.Email);

        if (email is null)
        {
            throw new UnauthorizedAccessException();
        }

        IEnumerable<string> roles = user
            .Claims
            .Where(c => c.Type == ClaimTypes.Role)!
            .Select(c => c.Value);

        return new CurrentUser(userId, email, roles);
    }
}
