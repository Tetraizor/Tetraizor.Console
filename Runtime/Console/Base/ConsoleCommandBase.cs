using UnityEngine;

namespace Tetraizor.Systems.Console.Base
{
    public abstract class ConsoleCommandBase : MonoBehaviour
    {
        public abstract bool Execute(string fullArgs);
    }
}
