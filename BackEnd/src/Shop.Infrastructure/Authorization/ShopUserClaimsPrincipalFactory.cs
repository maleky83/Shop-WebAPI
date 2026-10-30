using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shop.Domain.Entities;
using System.Security.Claims;

namespace Shop.Infrastructure.Authorization;

public class ShopUserClaimsPrincipalFactory(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options) :
        UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        ClaimsIdentity id = await GenerateClaimsAsync(user);

        if (user.Nationality != null)
        {
            id.AddClaim(new Claim("Nationality", user.Nationality));
        }

        if (user.DateOfBirth != null)
        {
            id.AddClaim(new Claim("DataOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }
}
