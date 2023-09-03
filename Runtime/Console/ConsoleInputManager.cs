using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleInputManager : MonoBehaviour
{
    #region Properties

    private ConsoleSystem _consoleSystem;

    [SerializeField] private InputActionReference _toggleConsoleAction;
    [SerializeField] private InputActionReference _submitAction;

    #endregion

    private void Update()
    {

        if (_toggleConsoleAction.action.WasPressedThisFrame())
        {
            _consoleSystem.ToggleConsole();
        }

        if (_submitAction.action.WasPressedThisFrame())
        {
            _consoleSystem.OnMessageSubmitRequested();
        }
    }

    #region Base Properties

    public void Init(ConsoleSystem consoleSystem)
    {
        _consoleSystem = consoleSystem;
    }

    #endregion
}
