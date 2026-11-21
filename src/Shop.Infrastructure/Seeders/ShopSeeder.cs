using Microsoft.AspNetCore.Identity;
using Shop.Application.Constants;
using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Seeders;

internal class ShopSeeder(ShopDbContext dbContext) : IShopSeeder
{
    public async Task Seed()
    {
        if (!dbContext.Roles.Any())
        {
            IEnumerable<IdentityRole> roles = GetRoles();
            await dbContext.AddRangeAsync(roles);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.Users.Any())
        {
            IEnumerable<User> users = GetUsers();
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.UserRoles.Any())
        {
            IEnumerable<IdentityUserRole<string>> userRoles = GetUserRoles();
            await dbContext.UserRoles.AddRangeAsync(userRoles);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = [
            new()
            {
                Id="0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Email="owner@gmail.com",
                UserName="owner@gmail.com",
                NormalizedEmail="owner@gmail.com".ToUpper(),
                NormalizedUserName="owner@gmail.com".ToUpper(),
                Nationality="iran",
                DateOfBirth=new DateOnly(2005,1,10),
                PasswordHash="AQAAAAIAAYagAAAAEPicYtOaWyOvVqKDzy46TUqdAZbRCKTdsDZl6qdCt4HYJRB+m3vLiiy9QNJlaggm+A==",
            },
            new()
            {
                Id="413da784-ecab-4248-b786-d5c6214b9786",
                Email="user@gmail.com",
                UserName="user@gmail.com",
                NormalizedEmail="user@gmail.com".ToUpper(),
                NormalizedUserName="user@gmail.com".ToUpper(),
                Nationality="iran",
                DateOfBirth=new DateOnly(2005,1,10),
                PasswordHash="AQAAAAIAAYagAAAAEPicYtOaWyOvVqKDzy46TUqdAZbRCKTdsDZl6qdCt4HYJRB+m3vLiiy9QNJlaggm+A==",
            },
            new()
            {
                Id="f875a48f-21b6-4ff2-8d42-bddabb8244f5",
                Email="admin@gmail.com",
                UserName="admin@gmail.com",
                NormalizedEmail="admin@gmail.com".ToUpper(),
                NormalizedUserName="admin@gmail.com".ToUpper(),
                Nationality="iran",
                DateOfBirth=new DateOnly(2005,1,10),
                PasswordHash="AQAAAAIAAYagAAAAEPicYtOaWyOvVqKDzy46TUqdAZbRCKTdsDZl6qdCt4HYJRB+m3vLiiy9QNJlaggm+A=="
            }
            ];
        return users;
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        IEnumerable<IdentityRole> roles = [
            new(UserRoles.Admin)
            {
                Id="8f5bbaf0-d4cb-4467-b674-6776d22cca93",
                NormalizedName=UserRoles.Admin.ToUpper()
            },
            new(UserRoles.User)
            {
                Id="6a47498d-96de-4b15-a5d0-acf1833e765b",
                NormalizedName=UserRoles.User.ToUpper()
            },
            new(UserRoles.Owner)
            {
                Id="987cb21b-9174-48be-bc7d-c2778bf24c82",
                NormalizedName=UserRoles.Owner.ToUpper()
            }];
        return roles;
    }

    private IEnumerable<IdentityUserRole<string>> GetUserRoles()
    {
        IEnumerable<IdentityUserRole<string>> userRoles = [
            new()
            {
                UserId="f875a48f-21b6-4ff2-8d42-bddabb8244f5",
                RoleId="8f5bbaf0-d4cb-4467-b674-6776d22cca93"

            },
            new()
            {
                RoleId="6a47498d-96de-4b15-a5d0-acf1833e765b",
                UserId="413da784-ecab-4248-b786-d5c6214b9786"
            },
            new()
            {
                RoleId="987cb21b-9174-48be-bc7d-c2778bf24c82",
                UserId="0af2138d-b32e-4f83-b6cd-3c162a3f6a0e"
            }];

        return userRoles;
    }
}
