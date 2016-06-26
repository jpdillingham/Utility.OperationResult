# Result.AddInfo Method 
 

Adds a message of type Info to the message list.

**Namespace:**&nbsp;<a href="846ea925-838c-f4a8-6a8a-689eb9584d48">Symbiote.OperationResult</a><br />**Assembly:**&nbsp;Symbiote.OperationResult (in Symbiote.OperationResult.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public virtual Result AddInfo(
	string message
)
```

**VB**<br />
``` VB
Public Overridable Function AddInfo ( 
	message As String
) As Result
```

**C++**<br />
``` C++
public:
virtual Result^ AddInfo(
	String^ message
)
```

**F#**<br />
``` F#
abstract AddInfo : 
        message : string -> Result 
override AddInfo : 
        message : string -> Result 
```


#### Parameters
&nbsp;<dl><dt>message</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />The message to add.</dd></dl>

#### Return Value
Type: <a href="fed882b9-fab1-b6e8-5855-cbc027039192">Result</a><br />This Result.

## Examples

```
// create a new Result
Result retVal = new Result();

// add an informational message
retVal.AddInfo("This is an informational message");
```


## See Also


#### Reference
<a href="fed882b9-fab1-b6e8-5855-cbc027039192">Result Class</a><br /><a href="846ea925-838c-f4a8-6a8a-689eb9584d48">Symbiote.OperationResult Namespace</a><br />