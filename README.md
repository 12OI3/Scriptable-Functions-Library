# Scriptable-Functions-Library V 0.1.5
Scriptable Functions Library is a tool that generates a ScriptableObject used to register and manage callable functions by string ID.

UPM git url: https://github.com/12OI3/Scriptable-Functions-Library.git?path=/Assets/Plugins/com.robsayyes.scriptablefunctionslibrary

# Introduction

Scriptable Functions Library is a tool that generates a ScriptableObject used to register and manage callable functions by string ID. It can support a variety of use cases, but it is a relatively simple strucutre to use. It is better suited for technical designers, as some parameters may need to be defined directly in code. This structure is not recommended for production environments, as it may not scale well for larger systems. However, it is very useful during prototyping, helping you avoid large switch-case statements and making function organization more manageable.

# Basic Utilities

You can click "Window > Scriptable Functions Library " in the menu to open the tool window. In order to use the tool, you will have to create library. An scriptable object will be created under the resource folde after you press the button, and every preset data will be stored here and utilize during runtime. It is important NOT to change the name and the path of the library.

This tool automatically registers all non-abstract classes that inherit from "ScriptableFunction". For example, you might define a function like this:

```csharp

using UnityEngine;
using ScriptableFunctionsLibrary;

public class Demo : MonoBehaviour
{
    void Start()
    {
        (ScriptableFunctionsLibraryManager.Library["DemoFunction"] as DemoFunction).Execute();
    }
}

public class DemoFunction : ScriptableFunction
{
    public void Execute()
    {
        Debug.Log("Hello World!");
    }
}

```

In this case, DemoFunction class will be searched and automatically register into the library. You can see the full list of the class on the tool window. Pressing the button can decide either enable or disable the class during runtime. The tool also support Unity;s Tooltip. You can add tool tip on each of the item in the library like following code:

```csharp

using UnityEngine;
using ScriptableFunctionsLibrary;

[Tooltip("This will print Hello World!")]
public class DemoToolTipFunction : ScriptableFunction
{
    public void Execute()
    {
        Debug.Log("Hello World!");
    }
}

```

# Abstract Usages

Since the tool will ignore all abstract class, it is recommand to implment base functions and parameters first with an abstract class inherits from ScriptableFunction, then having more class inherit from that abstract class. For example

```csharp

using UnityEngine;
using ScriptableFunctionsLibrary;

public abstract class NumberFunction : ScriptableFunction
{
    public abstract int Number { get;}
    public void PrintNumber()
    {
        Debug.Log(Number);
    }
}

public class One : NumberFunction
{
    public override int Number => 1;
}

public class Two : NumberFunction
{
    public override int Number => 2;
}

public class Three : NumberFunction
{
    public override int Number => 3;
}


```

# Async/Await

This strcuture tool support Async/Await! Check the following example:

``` csharp

using UnityEngine;
using ScriptableFunctionsLibrary;
using System.Threading.Tasks;

public abstract class AsyncFunction : ScriptableFunction
{
    public abstract Task Execute();
}

public class IsAsyncFunction : AsyncFunction
{
    public override async Task Execute()
    {
        await Task.Delay(1000);
    }
}

public class IsNotAsyncFunction : AsyncFunction
{
    public override Task Execute()
    {
        return Task.CompletedTask;
    }
}

```

# License

This library is under the MIT License.