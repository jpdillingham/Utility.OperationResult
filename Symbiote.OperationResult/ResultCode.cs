namespace Symbiote.OperationResult
{
    /// <summary>
    /// Defines the return result of an operation.
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// The default return type.
        /// </summary>
        Unknown,
        /// <summary>
        /// The operation succeeded.
        /// </summary>
        Success,
        /// <summary>
        /// The operation encountered recoverable issues and ultimately succeeded.
        /// </summary>
        Warning,
        /// <summary>
        /// The operation encountered unrecoverable errors and did not succeed.
        /// </summary>
        Failure
    }
}
