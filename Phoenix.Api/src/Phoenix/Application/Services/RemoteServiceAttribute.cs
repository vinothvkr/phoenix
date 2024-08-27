using System.Reflection;

namespace Phoenix.Application.Services;

public class RemoteServiceAttribute : Attribute
{
    /// <summary>
    /// Default: true.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Default: true.
    /// </summary>
    public bool IsMetadataEnabled { get; set; }

    public RemoteServiceAttribute(bool isEnabled = true)
    {
        IsEnabled = isEnabled;
        IsMetadataEnabled = true;
    }

    public virtual bool IsEnabledFor(Type type)
    {
        return IsEnabled;
    }

    public virtual bool IsEnabledFor(MethodInfo method)
    {
        return IsEnabled;
    }
}
