using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }

    public bool IsActive { get; set; } = true;

    #region Relations

    public Cart? Cart { get; set; }

    public ICollection<Orders.Order> Orders { get; }
        = new List<Orders.Order>();

    #endregion
}
