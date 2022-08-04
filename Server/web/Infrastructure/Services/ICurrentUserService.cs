namespace web.Infrastructure.Services;

public interface ICurrentUserService
{
    string GetUserName();

    string GetUserId();
}