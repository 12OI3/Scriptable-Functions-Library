using UnityEngine;
using ScriptableFunctionsLibrary;
using System.Threading.Tasks;

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

[Tooltip("This will print Hello World!")]
public class DemoToolTipFunction : ScriptableFunction
{
    public void Execute()
    {
        Debug.Log("Hello World!");
    }
}

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