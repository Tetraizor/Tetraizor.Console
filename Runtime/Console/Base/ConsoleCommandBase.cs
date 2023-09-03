using UnityEngine;

public abstract class ConsoleCommandBase : MonoBehaviour
{
    public abstract bool Execute(string fullArgs);
}
