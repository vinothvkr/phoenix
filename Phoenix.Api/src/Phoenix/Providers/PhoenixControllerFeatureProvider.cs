using Microsoft.AspNetCore.Mvc.Controllers;
using Phoenix.Application.Services;
using System.Reflection;

namespace Phoenix.Providers;

internal class PhoenixControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        if (!typeInfo.IsClass)
        {
            return false;
        }
        if (typeInfo.IsAbstract)
        {
            return false;
        }
        if (typeInfo.ContainsGenericParameters)
        {
            return false;
        }
        if (!typeof(IApplicationService).IsAssignableFrom(typeInfo.AsType()))
        {
            return false;
        }
        return true;
    }
}
