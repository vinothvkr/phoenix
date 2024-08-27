using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Phoenix.Application.Services;
using Phoenix.Extensions;
using Phoenix.Helpers;
using Phoenix.Reflection;
using System.Reflection;

namespace Phoenix.Conventions;

public class PhoenixAppServiceConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            Type? type = controller.ControllerType.AsType();
            
            if (typeof(IApplicationService).GetTypeInfo().IsAssignableFrom(type))
            {
                controller.ControllerName = controller.ControllerName.RemovePostFix(ApplicationService.CommonPostfixes);

                ConfigureRemoteService(controller);
            }
        }
    }

    private void ConfigureRemoteService(ControllerModel controller)
    {
        ConfigureSelector(controller);
    }

    private void ConfigureSelector(ControllerModel controller)
    {
        RemoveEmptySelectors(controller.Selectors);

        if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
        {
            return;
        }

        foreach (var action in controller.Actions)
        {
            ConfigureSelector(controller.ControllerName, action);
        }
    }

    private void ConfigureSelector(string controllerName, ActionModel action)
    {
        RemoveEmptySelectors(action.Selectors);

        var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(action.ActionMethod);
        if (remoteServiceAtt != null && !remoteServiceAtt.IsEnabledFor(action.ActionMethod))
        {
            return;
        }

        if (!action.Selectors.Any())
        {
            AddServiceSelector(controllerName, action);
        }
        else
        {
            NormalizeSelectorRoutes(controllerName, action);
        }
    }

    private void AddServiceSelector(string controllerName, ActionModel action)
    {
        var serviceSelectorModel = new SelectorModel
        {
            AttributeRouteModel = CreateServiceAttributeRouteModel(controllerName, action)
        };

        var httpMethod = SelectHttpMethod(action);

        serviceSelectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod }));

        action.Selectors.Add(serviceSelectorModel);
    }

    private void NormalizeSelectorRoutes(string controllerName, ActionModel action)
    {
        foreach (var selector in action.Selectors)
        {
            if (!selector.ActionConstraints.OfType<HttpMethodActionConstraint>().Any())
            {
                var httpMethod = SelectHttpMethod(action);
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod }));
            }

            if (selector.AttributeRouteModel == null)
            {
                selector.AttributeRouteModel = CreateServiceAttributeRouteModel(
                    controllerName,
                    action
                );
            }
        }
    }

    private static void RemoveEmptySelectors(IList<SelectorModel> selectors)
    {
        selectors
            .Where(IsEmptySelector)
            .ToList()
            .ForEach(s => selectors.Remove(s));
    }

    private static bool IsEmptySelector(SelectorModel selector)
    {
        return selector.AttributeRouteModel == null
               && selector.ActionConstraints.IsNullOrEmpty()
               && selector.EndpointMetadata.IsNullOrEmpty();
    }

    private static AttributeRouteModel CreateServiceAttributeRouteModel(string controllerName, ActionModel action)
    {
        return new AttributeRouteModel(
            new RouteAttribute(
                $"api/services/{controllerName}/{action.ActionName}"
            )
        );
    }

    private string SelectHttpMethod(ActionModel action)
    {
        return ProxyScriptingHelper.GetConventionalVerbForMethodName(action.ActionName);
    }
}
