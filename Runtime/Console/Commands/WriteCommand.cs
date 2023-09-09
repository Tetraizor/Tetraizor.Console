using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tetraizor.Systems.Console.Base;

namespace Tetraizor.Systems.Console.Commands
{
    public class WriteCommand : ConsoleCommandBase
    {
        public override bool Execute(string fullArgs)
        {
            ConsoleSystem.Instance.WriteLine(fullArgs + '\n');
            return true;
        }
    }
}