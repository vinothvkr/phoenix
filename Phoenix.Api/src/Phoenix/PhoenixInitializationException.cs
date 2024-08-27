using System.Runtime.Serialization;

namespace Phoenix;

/// <summary>
/// This exception is thrown if a problem on Phoenix initialization progress.
/// </summary>
public class PhoenixInitializationException : PhoenixException
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PhoenixInitializationException()
    {

    }

    /// <summary>
    /// Constructor for serializing.
    /// </summary>
    public PhoenixInitializationException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    public PhoenixInitializationException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public PhoenixInitializationException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
