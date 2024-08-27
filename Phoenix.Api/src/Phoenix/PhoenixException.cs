using System.Runtime.Serialization;

namespace Phoenix;

/// <summary>
/// Base exception type for those are thrown by Phoenix system for Phoenix specific exceptions.
/// </summary>
public class PhoenixException : Exception
{
    /// <summary>
    /// Creates a new <see cref="PhoenixException"/> object.
    /// </summary>
    public PhoenixException()
    {

    }

    /// <summary>
    /// Creates a new <see cref="PhoenixException"/> object.
    /// </summary>
    public PhoenixException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Creates a new <see cref="PhoenixException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public PhoenixException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="PhoenixException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public PhoenixException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
