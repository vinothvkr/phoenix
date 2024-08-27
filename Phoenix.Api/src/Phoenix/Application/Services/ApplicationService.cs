using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Phoenix.Application.Services;

public abstract class ApplicationService : IApplicationService
{
    public static string[] CommonPostfixes = ["AppService", "ApllicationService"];

    public HttpContext HttpContext {  get; set; }

    public ApplicationService(IServiceProvider serviceProvider)
    {
        HttpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
    }
}
