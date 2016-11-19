# OperationResult

[![Build status](https://ci.appveyor.com/api/projects/status/vy64nop0tqeqdq9i?svg=true)](https://ci.appveyor.com/project/jpdillingham/operationresult)
[![codecov](https://codecov.io/gh/jpdillingham/OperationResult/branch/master/graph/badge.svg)](https://codecov.io/gh/jpdillingham/OperationResult)
[![Dependency Status](https://www.versioneye.com/user/projects/581c04a04304530b557dc736/badge.svg?style=flat-square)](https://www.versioneye.com/user/projects/581c04a04304530b557dc736)
[![NuGet version](https://badge.fury.io/nu/Utility.OperationResult.svg)](https://badge.fury.io/nu/Utility.OperationResult)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/jpdillingham/Utility.OperationResult/blob/master/LICENSE)

The OperationResult namespace contains two types; the ```Result``` type and the generic extension type ```Result{T}```.

Both types are designed to be used as method return types.  Both types contain a ```ResultCode``` enumeration used to describe the result of the operation in terms of success, success with warnings, or failure, and a ```List{T}``` of type ```Message``` containing a list of messages generated during the operation.

These types allow the programmer to defer logging to other parts of the application code, as well as allowing more detailed information about
the operation to be conveyed to calling members without throwing expensive exceptions or passing special values in return types.

This library depends on [NLog](https://www.nuget.org/packages/NLog/) for logging functions.

## OperationResult.Result

Represents the result of an operation, including a result code and list of messages generated during the operation.

The primary function of the Result is to store the result of the operation in the ```Result.ResultCode```
property.  This property is of type ```ResultCode```, which has members ```ResultCode.Success```,
which represents successful operations, <see cref="ResultCode.Warning"/>, which represents operations that succeeded
but generated warning messages while executing, and <see cref="ResultCode.Failure"/>, which represents operations that failed.

Operations may also generate messages as they execute.  These messages are stored in the ```Result.Messages``` property as a ```List{T}```
of type ```Message```.  Each message consists of an ```MessageType``` representing the type of message
(informational with ```MessageType.Info```, warning with ```MessageType.Warning```, and errors with
```MessageType.Error```), and a string containing the message itself.

Messages can be added to the Result with the ```AddInfo(string)```, ```AddWarning(string)``` and ```AddError(string)```
methods.  The ```AddWarning()``` and ```AddError()``` messages automatically change the ResultCode to Warning and Failure when invoked, respectively.

Several shorthand logging methods are provided, namely ```LogResult(NLog.Logger, string)``` and it's overloads, and 
```LogAllMessages(Action{string}, string, string)```.  These methods are designed to leverage NLog, however overloads are provided
so that most logging functionality can be used by supplying a delegate method which accepts a string parameter.

The ```Incorporate(Result)``` method is provided so that Result objects can be merged with one another.  The instance
on which the ```Incorporate()``` method is invoked will copy all messages from the specified Result into it's list, and if the ResultCode
of the specified Result is "less than" that of the current instance, the instance will take on the new ResultCode.  For instance, if the invoking
instance has a ResultCode of Warning and an Result with a ResultCode of Failure is incorporated, the ResultCode of the invoking instance
will be changed to Failure.  This functionality is provided for nested or sequential operations.

#### OperationResult.Result&lt;T&gt;

The generic version of Result, ```Result{T}```, accepts a single type parameter and includes an additional property corresponding
to the specified type in ```ReturnValue```.  This functionality is provided for operations which have a return value other than void, allowing these methods to return the original return value in addition to the Result.  This version also includes the ```SetReturnValue(T)``` method, which sets the value of the Result property to the specified value.  The property may also be set directly; this method, however, allows for fluent API usage.

### Methods

#### AddInfo()
Adds a message of type Info to the message list.
##### Example

```c#
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddInfo("This is an informational message");
```

#### AddWarning()

Adds a message of type Warning to the message list and sets the ResultCode to Warning.

##### Example
```c#
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddWarning("This is a warning message");
```

#### AddError()

Adds a message of type Error to the message list and sets the ResultCode to Error.

##### Example
```c#
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddError("This is an error message");
```

#### RemoveMessages()

Removes all messages of the optionally specifiied MessageType, or all messages if MessageType is not specified.

##### Example
```c#
// create a new Result
Result retVal = new Result();
 
// add a few messages
retVal.AddError("This is an error message");
retVal.AddError("This is another error message");
 
// remove the messages that were just added
retVal.RemoveMessages(MessageType.Error);
```
#### SetResultCode()

Sets the ResultCode property to the optionally supplied ResultCode, or to ResultCode.Success if ResultCode is not specified.

##### Example
```c#
// create a new Result and initialize the ResultCode to ResultCode.Failure
Result retVal = new Result().SetResultCode(ResultCode.Failure)
```

#### LogResult()

Logs the result of the operation using the specified logger instance and the optionally specified caller as the source.

The default logging methods are applied to corresponding message types; Info for Info, Warn for Warning and Error for Errors.

The caller parameter is automatically set to the calling method.  In some cases, such as when a result for a method
is logged within a method different from the executing method, this will need to be explicitly specified
to reflect the actual source of the Result.

If a logger different from NLog is desired, modify the type of the logger parameter accordingly and substitute
the appropriate methods for info, warn and error log levels (assuming they are applicable).

##### Example
```c#
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddInfo("This is an informational message");

// log the result
// use logger.Info for basic and informational messages, logger.Warn for warnings
// and logger.Error for errors.
retVal.LogResult(logger);
```

##### Example Output
The following is an example of the output from the ```LogResult()``` method, taken from the included Example project:

```
[INFO] [x]: The operation 'ResultExample' completed successfully.
[INFO] [x]: The following informational messages were generated during the operation:
[INFO] [x]:      The lucky number is: 95000
[INFO] [x]:      Attempts: 989
```

#### LogAllMessages()

Logs all messages in the message list to the specified logging method.  If specified, logs a header and footer message before and after the list, respectively.

##### Example
```c#
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddInfo("This is an informational message");

// add a warning
retVal.AddWarning("This is a warning");

// log the list of messages with the Info logging level
// include a header and footer
retVal.LogAllMessages(logger.Info, "Message list:", "End of list.");
```

#### GetLast\[Info|Warning|Error\]()

Returns the most recently added informational, warning or error message contained within the message list.

##### Example
```c#
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddInfo("This is an informational message");

// print the last info message
Console.WriteLine(retVal.GetLastInfo());
```

#### Incorporate()

Adds details from the specified Result to this Result, including all Messages and the 
ResutCode, if lesser than the ResultCode of this instance.

##### Example
```c#
// create an "outer" Result
// the ResultCode of this instance is Success by default.
Result outer = new Result();

// ... some logic ...

// create an "inner" Result
// set this to the result of a different method
Result inner = MyMethod();

// incorporate the inner Result into the outer
// this copies all messages and, if the inner instance's ResultCode
// is lesser (Success > Warning > Failure) than the outer, copies the ResultCode as well.
outer.Incorporate(inner);

// log the result.  the combined list of messages from both inner and outer
// are logged, and the ResultCode is equal to the lesser of the two ResultCodes.
outer.LogResult(logger); 
```

#### SetReturnValue (Result(T) only)

Sets the ReturnValue property to the specified value.

##### Example
```c#
//create a new Result
Result<string> result = new Result<string>()

result
  .SetReturnValue("Hello World!")
  .AddInfo("Set value.")
  .LogResult(logger.Info);
```

# Notes

## Other Loggers

NLog is currently integrated, however another logging library (or none at all) can be added easily by modifying (or removing) the ```LogResult()``` overload which accepts a logger as the first parameter.  Change the type to another logger and modify the ```Action<string>``` delegates sent to the primary overload, or, if no logging integration is desired, remove the method.

## Logging From Non-Originating Methods

The ```LogResult()``` method uses the ```[CallerMemberName]``` implicit parameter to determing the name of the calling method.  This works perfectly fine if ```LogResult()``` is invoked within the method which instantiated the ```Result```, however if this method is invoked by some other method, the name of the result will be incorrect when it is logged.

To force the correct name, supply the originating method name as the second parameter for ```LogResult()```:

```c#
result.LogResult(logger, "OriginatingMethodName");
```

This will replace the implicit parameter ```[CallerMemberName]``` with the provided name.
