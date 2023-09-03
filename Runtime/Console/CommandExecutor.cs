using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    #region Properties

    private Dictionary<string, ConsoleCommandBase> _commands = new();
    private ConsoleSystem _consoleSystem;

    #endregion

    #region Private Methods

    private ConsoleCommandBase GetCommandFromName(string commandName)
    {
        if (_commands.ContainsKey(commandName))
            return _commands[commandName];
        else
            return null;
    }

    #endregion

    #region Base Methods

    public void Init(ConsoleSystem consoleSystem)
    {
        // Register commands.
        ConsoleCommandBase[] commands = GetComponentsInChildren<ConsoleCommandBase>();

        foreach (ConsoleCommandBase command in commands)
        {
            _commands.Add(command.name, command);
        }

        // Assign references.
        _consoleSystem = consoleSystem;
    }

    public bool ExecuteCommand(string commandFullLine)
    {
        string commandName;
        string commandParameters;

        // Check if command is empty.
        if (commandFullLine.Trim() == "" || commandFullLine == null)
        {
            _consoleSystem.WriteLine("", true);
            return false;
        }

        // Write inputted command as it is.
        _consoleSystem.WriteLine($"<color=#BABABA>{commandFullLine}</color>", true);

        if (commandFullLine.Contains(' '))
        {
            string[] splitCommand = commandFullLine.Split(' ', 2);

            commandName = splitCommand[0];
            commandParameters = splitCommand[1];
        }
        else
        {
            commandName = commandFullLine;
            commandParameters = null;
        }

        // Find and execute command.
        ConsoleCommandBase command = GetCommandFromName(commandName);

        if (command == null)
        {
            _consoleSystem.WriteLine("<color=#DC4545>Invalid command.</color>\n");
            return false;
        }

        command.Execute(commandParameters);
        return true;
    }

    #endregion
}
