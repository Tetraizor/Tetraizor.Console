using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tetraizor.Systems.Console.Base;

namespace Tetraizor.Systems.Console.Commands
{
    public class ClearCommand : ConsoleCommandBase
    {
        public override bool Execute(string fullArgs)
        {
            ConsoleSystem.Instance.ClearConsole();
            return true;
        }
    }
}
