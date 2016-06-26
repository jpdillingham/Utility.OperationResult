# Symbiote.OperationResult
C# Class Library built to encapsulate the result of any operation. Includes a result code and a list of messages generated during the operation.

The OperationResult namespace contains two types; the ```Result``` type and the generic extension type ```Result{T}```.

Both types are designed to be used as method return types.  Both types contain a ```ResultCode``` enumeration used to describe the result of the operation in terms of success, success with warnings, or failure, and a ```List{T}"``` of type ```Message``` containing a list of messages generated during the operation.

These types allow the programmer to defer logging to other parts of the application code, as well as allowing more detailed information about
the operation to be conveyed to calling members without throwing expensive exceptions or passing special values in return types.

This library depends on [NLog](https://www.nuget.org/packages/NLog/) for logging functions.

# Utilization

## Installing

Build the project, then add a reference to the project to which you want to add OperationResult.

## Referencing

Add a reference to the namespace to the file in which you wish to use the class:

```c#
using Symbiote.OperationResult;
```

## Instantiating

Create a new, typeless OperationResult for a method with an otherwise void return type:

```c#

public Result MyMethod()
{
  Result myResult = new Result();
  return myResult;
}
```

Create a new, typed OperationResult for a method with a return type:

```c#

public Result<string> MyStringMethod()
{
  Result<string> myResult = new Result<string>();
  return myResult;
}
```
