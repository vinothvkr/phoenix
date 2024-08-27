using Phoenix.Application.Services;

namespace Phoenix.Application.Users;

public class UserAppService(IServiceProvider serviceProvider) : ApplicationService(serviceProvider), IUserAppService
{
    public string GetUser()
    {
        return "Hello World!";
    }

    public string UpdateUser()
    {
        return "Hello World!";
    }

    public string DeleteUser()
    {
        return "Hello World!";
    }
}
