using System.Collections;
using UnityEngine;
using Tetraizor.DebugUtils.Base;
using Tetraizor.Bootstrap.Base;
using Tetraizor.DebugUtils;

namespace Tetraizor.Systems.Console.Subsystems
{
    public class ConsoleDebuggerSubsystem : MonoBehaviour, IDebugger, IPersistentSubsystem
    {
        #region IDebugger Methods

        public void OnPrintMessageEmitted(string message)
        {
            ConsoleSystem.Instance.WriteLine(message);
        }

        public void OnSuccessMessageEmitted(string message)
        {
            ConsoleSystem.Instance.WriteLine("<color=#62E176>" + message + "</color>");
        }

        public void OnErrorMessageEmitted(string message)
        {
            ConsoleSystem.Instance.WriteLine("<color=#DC4545>" + message + "</color>");
        }

        public void OnWarningMessageEmitted(string message)
        {
            ConsoleSystem.Instance.WriteLine("<color=#F0DA66>" + message + "</color>");
        }

        #endregion

        #region IPersistentSubsystem Methods

        public string GetSystemName()
        {
            return "Console System";
        }

        public IEnumerator LoadSubsystem(IPersistentSystem system)
        {
            DebugBus.Register(this);
            DebugBus.LogPrint("Console Debugger registered successfully.");

            yield return null;
        }

        #endregion
    }
}