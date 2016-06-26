namespace OperationResult
{
    /// <summary>
    /// Defines the message type for an operation message.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The default type; represents any level.
        /// </summary>
        Any,
        /// <summary>
        /// The message contains low level trace information.
        /// </summary>
        Info,
        /// <summary>
        /// The message represents a recoverable issue.
        /// </summary>
        Warning,
        /// <summary>
        /// The message represents an uncoverable error.
        /// </summary>
        Error
    }

}
