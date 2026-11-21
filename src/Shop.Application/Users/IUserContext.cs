namespace Shop.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}