using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteCommand : ConsoleCommandBase
{
    public override bool Execute(string fullArgs)
    {
        ConsoleSystem.Instance.WriteLine(fullArgs + '\n');
        return true;
    }
}
