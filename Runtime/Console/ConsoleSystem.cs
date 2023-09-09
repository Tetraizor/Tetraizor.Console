using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Tetraizor.MonoSingleton;
using Tetraizor.Bootstrap.Base;
using Tetraizor.Systems.Console.Subsystems;

namespace Tetraizor.Systems.Console
{
    public class ConsoleSystem : MonoSingleton<ConsoleSystem>, IPersistentSystem
    {
        #region Properties

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _consoleText;
        [SerializeField] private TMP_InputField _consoleInput;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Button _submitButton;

        [Header("Component References")]
        [SerializeField] private CommandExecutorSubsystem _commandExecutor;
        [SerializeField] private Animator _consoleUIAnimator;

        // Console Animator parameter hashes 
        private int _toggleOnTrigger = 0;
        private int _toggleOffTrigger = 0;

        // Console state parameters
        private bool _isOn = false;
        public bool IsOn => _isOn;

        #endregion

        #region Console Related Methods

        public void WriteLine(string command, bool isUserInput = false)
        {
            _consoleText.SetText(_consoleText.text + (isUserInput ? "> " : "") + command + "\n");

            StartCoroutine(UpdateConsoleText());
        }

        public void ClearConsole()
        {
            _consoleText.SetText("");
        }

        private void ClearInput()
        {
            _consoleInput.SetTextWithoutNotify("");
        }

        public void ToggleConsole()
        {
            ToggleConsole(!_isOn);
        }

        public void ToggleConsole(bool state)
        {
            if (state == _isOn)
                return;
            else
                _isOn = state;

            if (_isOn) // On
            {
                ToggleConsoleFunctionality(true);
                ToggleConsoleVisuals(true);
            }
            else // Off
            {
                ToggleConsoleFunctionality(false);
                ToggleConsoleVisuals(false);
            }

            ClearInput();
        }

        private void ToggleConsoleVisuals(bool state)
        {
            if (state)
            {
                _consoleUIAnimator.SetTrigger(_toggleOnTrigger);
            }
            else
            {
                _consoleUIAnimator.SetTrigger(_toggleOffTrigger);
            }

            StartCoroutine(UpdateConsoleText());
        }

        private void ToggleConsoleFunctionality(bool state)
        {
            if (state)
            {
                _consoleInput.Select();

                _submitButton.interactable = true;
                _consoleInput.interactable = true;
            }
            else
            {
                _submitButton.interactable = false;
                _consoleInput.interactable = false;

                if (EventSystem.current != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                {
                    print("No EventSystem present in current scene.");
                }
            }
        }

        private IEnumerator UpdateConsoleText()
        {
            yield return new WaitForEndOfFrame();
            _scrollRect.verticalNormalizedPosition = 0;
            _consoleInput.ActivateInputField();
        }

        #endregion

        #region UI Event Callbacks

        public void OnMessageSubmitRequested()
        {
            _commandExecutor.ExecuteCommand(_consoleInput.text);
            ClearInput();
        }

        #endregion

        #region IPersistentSystem Methods

        public string GetName()
        {
            return "Console System";
        }

        public IEnumerator LoadSystem()
        {
            // Assign event callbacks.
            _submitButton.onClick.AddListener(OnMessageSubmitRequested);

            // Initialize properties.
            _toggleOnTrigger = Animator.StringToHash("ConsoleOn");
            _toggleOffTrigger = Animator.StringToHash("ConsoleOff");

            _isOn = false;
            ToggleConsoleFunctionality(false);

            // Initialize subsystems.
            IPersistentSubsystem[] subsystems = gameObject.GetComponentsInChildren<IPersistentSubsystem>(true);

            foreach (IPersistentSubsystem subsystem in subsystems)
            {
                yield return subsystem.LoadSubsystem(this);
            }

            yield return null;
        }

        #endregion
    }
}