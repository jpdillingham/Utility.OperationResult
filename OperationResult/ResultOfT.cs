using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OperationResult
{
    /// <summary>
    /// Represents the result of an operation, including a result code and list of messages generated during the operation.
    /// </summary>
    /// <remarks>
    /// <para>
    ///     The primary function of the Result is to store the result of the operation in the <see cref="Result.ResultCode"/>
    ///     property.  This property is of type <see cref="ResultCode"/>, which has members <see cref="ResultCode.Success"/>,
    ///     which represents successful operations, <see cref="ResultCode.Warning"/>, which represents operations that succeeded
    ///     but generated warning messages while executing, and <see cref="ResultCode.Failure"/>, which represents operations that failed.
    /// </para>
    /// <para>
    ///     Operations may also generate messages as they execute.  These messages are stored in the <see cref="Result.Messages"/> property as a <see cref="List{T}"/>
    ///     of type <see cref="Message"/>.  Each message consists of an <see cref="MessageType"/> representing the type of message
    ///     (informational with <see cref="MessageType.Info"/>, warning with <see cref="MessageType.Warning"/>, and errors with
    ///     <see cref="MessageType.Error"/>), and a string containing the message itself.
    /// </para>
    /// <para>
    ///     Messages can be added to the Result with the <see cref="AddInfo(string)"/>, <see cref="AddWarning(string)"/> and <see cref="AddError(string)"/>
    ///     methods.  The AddWarning() and AddError() messages automatically change the ResultCode to Warning and Failure when invoked, respectively.
    /// </para>
    /// <para>
    ///     Several shorthand logging methods are provided, namely <see cref="LogResult(NLog.Logger, string)"/> and it's overloads, and 
    ///     <see cref="LogAllMessages(Action{string}, string, string)"/>.  These methods are designed to leverage NLog, however overloads are provided
    ///     so that most logging functionality can be used by supplying a delegate method which accepts a string parameter.
    /// </para>
    /// <para>
    ///     The <see cref="Incorporate(Result)"/> method is provided so that Result objects can be merged with one another.  The instance
    ///     on which the Incorporate() method is invoked will copy all messages from the specified Result into it's list, and if the ResultCode
    ///     of the specified Result is "less than" that of the current instance, the instance will take on the new ResultCode.  For instance, if the invoking
    ///     instance has a ResultCode of Warning and an Result with a ResultCode of Failure is incorporated, the ResultCode of the invoking instance
    ///     will be changed to Failure.  This functionality is provided for nested or sequential operations.
    /// </para>
    /// <para>
    ///     The generic version of Result, <see cref="Result{T}"/>, accepts a single type parameter and includes an additional property corresponding
    ///     to the specified type in <see cref="ReturnValue"/>.  This functionality is provided for operations which have a return value other than void, allowing these 
    ///     methods to return the original return value in addition to the Result.  This version also includes the <see cref="SetReturnValue(T)"/> method, which 
    ///     sets the value of the Result property to the specified value.  The property may also be set directly; this method, however, allows for fluent API usage.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The type of the object contained within the Result property.</typeparam>
    public class Result<T> : Result
    {
        #region Properties

        /// <summary>
        /// An object containing the result of the operation.
        /// </summary>
        public T ReturnValue { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a default Result.
        /// </summary>
        public Result() : base()
        {
            ReturnValue = default(T);
        }

        #endregion

        #region Instance Methods

        #region Public 

        /// <summary>
        /// Adds a message of type Info to the message list.
        /// </summary>
        /// <param name="message">The message to add.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddInfo("This is an informational message");
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> AddInfo(string message)
        {
            base.AddInfo(message);
            return this;
        }

        /// <summary>
        /// Adds a message of type Warning to the message list and sets the ResultCode to Warning.
        /// </summary>
        /// <param name="message">The message to add.</param>
        /// <returns>This Result</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddWarning("This is a warning message");
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> AddWarning(string message)
        {
            base.AddWarning(message);
            return this;
        }

        /// <summary>
        /// Adds a message of type Error to the message list and sets the ResultCode to Error.
        /// </summary>
        /// <param name="message">The message to add.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddError("This is an error message");
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> AddError(string message)
        {
            base.AddError(message);
            return this;
        }

        /// <summary>
        ///     Logs the result of the operation using the specified logger instance and the optionally specified caller as the source.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     The default logging methods are applied to corresponding message types; Info for Info, Warn for Warning and Error for Errors.
        /// </para>
        /// <para>
        ///     The caller parameter is automatically set to the calling method.  In some cases, such as when a result for a method
        ///     is logged within a method different from the executing method, this will need to be explicitly specified
        ///     to reflect the actual source of the Result.
        /// </para>
        /// <para>
        ///     If a logger different from NLog is desired, modify the type of the logger parameter accordingly and substitute
        ///     the appropriate methods for info, warn and error log levels (assuming they are applicable).
        /// </para>
        /// </remarks>
        /// <param name="logger">The logger with which to log the result.</param>
        /// <param name="caller">The name of calling method.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddInfo("This is an informational message");
        /// 
        /// // log the result
        /// // use logger.Info for basic and informational messages, logger.Warn for warnings
        /// // and logger.Error for errors.
        /// retVal.LogResult(logger);
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> LogResult(NLog.Logger logger, [CallerMemberName]string caller = "")
        {
            base.LogResult(logger, caller);
            return this;
        }

        /// <summary>
        ///     Logs the result of the operation using the specified logging method and the optionally specified caller as the source.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     The specified logging method is applied to all message types (Info, Warning, and Error).
        /// </para>
        /// <para>
        ///     The caller parameter is automatically set to the calling method.  In some cases, such as when a result for a method
        ///     is logged within a method different from the executing method, this will need to be explicitly specified
        ///     to reflect the actual source of the Result.
        /// </para>
        /// </remarks>
        /// <param name="action">The logging method with which to log the result.</param>
        /// <param name="caller">The name of the calling method.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddInfo("This is an informational message");
        /// 
        /// // log the result using the Debug logging level for all message types.
        /// retVal.LogResult(logger.Debug);
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> LogResult(Action<string> action, [CallerMemberName]string caller = "")
        {
            base.LogResult(action, caller);
            return this;
        }

        /// <summary>
        ///     Logs the result of the operation using the three specified logging methods and the optionally specified caller as the source.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     The first, second and third specified logging methods are applied to messages of type Info, Warning and Error, respectively.
        /// </para>
        /// <para>
        ///     The caller parameter is automatically set to the calling method.  In some cases, such as when a result for a method
        ///     is logged within a method different from the executing method, this will need to be explicitly specified
        ///     to reflect the actual source of the Result.
        /// </para>
        /// </remarks>
        /// <param name="successAction">The logging method with which to log successful messages.</param>
        /// <param name="warningAction">The logging method with which to log warning messages.</param>
        /// <param name="failureAction">The logging method with which to log messages.</param>
        /// <param name="caller">The name of the calling method.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddInfo("This is an informational message");
        /// 
        /// // log the result
        /// // use logger.Trace for basic and informational messages, logger.Debug for warnings
        /// // and logger.Warn for errors.
        /// retVal.LogResult(logger.Trace, logger.Debug, logger.Warn);
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result LogResult(Action<string> successAction, Action<string> warningAction, Action<string> failureAction, [CallerMemberName]string caller = "")
        {
            base.LogResult(successAction, warningAction, failureAction, caller);
            return this;
        }

        /// <summary>
        /// Logs all messages in the message list to the specified logging method.  If specified, logs a header and footer message before and after the list, respectively.
        /// </summary>
        /// <param name="action">The logging method with which to log the messages.</param>
        /// <param name="header">A header message to log prior to the list of messages.</param>
        /// <param name="footer">A footer message to display after the list of messages.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create a new Result<T>
        /// Result<object> retVal = new Result<object>();
        /// 
        /// // add an informational message
        /// retVal.AddInfo("This is an informational message");
        /// 
        /// // add a warning
        /// retVal.AddWarning("This is a warning");
        /// 
        /// // log the list of messages with the Info logging level
        /// // include a header and footer
        /// retVal.LogAllMessages(logger.Info, "Message list:", "End of list.");
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> LogAllMessages(Action<string> action, string header = "", string footer = "")
        {
            base.LogAllMessages(action, header, footer);
            return this;
        }

        /// <summary>
        ///     Adds details from the specified Result to this Result, including all Messages and the 
        ///     ResutCode, if lesser than the ResultCode of this instance.
        /// </summary>
        /// <param name="Result">The Result from which to copy the Messages.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // create an "outer" Result<T>
        /// // the ResultCode of this instance is Success by default.
        /// Result<object> outer = new Result<object>();
        /// 
        /// // ... some logic ...
        /// 
        /// // create an "inner" Result<T>
        /// // set this to the result of a different method
        /// Result<object> inner = MyMethod<object>();
        /// 
        /// // incorporate the inner Result into the outer
        /// // this copies all messages and, if the inner instance's ResultCode
        /// // is lesser (Success > Warning > Failure) than the outer, copies the ResultCode as well.
        /// outer.Incorporate(inner);
        /// 
        /// // log the result.  the combined list of messages from both inner and outer
        /// // are logged, and the ResultCode is equal to the lesser of the two ResultCodes.
        /// outer.LogResult(logger); 
        /// ]]>
        /// </code>
        /// </example>
        new public virtual Result<T> Incorporate(Result Result)
        {
            base.Incorporate(Result);
            return this;
        }

        /// <summary>
        /// Sets the ReturnValue property to the specified value.
        /// </summary>
        /// <param name="returnValue">The value to which the Result property is to be set.</param>
        /// <returns>This Result.</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// ///create a new Result
        /// Result<string> result = new Result<string>()
        /// result
        ///   .SetReturnValue("Hello World!")
        ///   .AddInfo("Set value.")
        ///   .LogResult(logger.Info);
        /// ]]>
        /// </code>
        /// </example>
        public Result<T> SetReturnValue(T returnValue)
        {
            ReturnValue = returnValue;
            return this;
        }

        #endregion

        #endregion
    }
}
