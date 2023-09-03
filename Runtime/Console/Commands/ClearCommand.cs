using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCommand : ConsoleCommandBase
{
    public override bool Execute(string fullArgs)
    {
        ConsoleSystem.Instance.ClearConsole();
        return true;
    }
}
