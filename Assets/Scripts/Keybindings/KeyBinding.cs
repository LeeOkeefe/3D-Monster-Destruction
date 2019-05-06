using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Keybindings
{
    internal sealed class KeyBinding : MonoBehaviour
    {
        public static KeyBinding Instance { get; private set; }

        [SerializeField] private Text m_Forward, m_Back, m_Left, m_Right;
        [SerializeField] private Text m_Punch, m_Jump, m_Sprint, m_Pickup, m_Throw;
        [SerializeField] private Text m_Ability1, m_Ability2, m_Ability3, m_Ability4;

        [SerializeField] private CanvasGroup m_ErrorMessage;

        private GameObject m_CurrentKey;

        private void Start()
        {
            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(this);

            

            UpdateLabels();
        }

        /// <summary>
        /// Update Text components of the custom input buttons
        /// </summary>
        public void UpdateLabels()
        {
            var keyCodes = KeybindMap.KeyCodes;

            m_Forward.text = keyCodes["Forward"].ToString();
            m_Back.text = keyCodes["Back"].ToString();
            m_Left.text = keyCodes["Left"].ToString();
            m_Right.text = keyCodes["Right"].ToString();
            m_Punch.text = keyCodes["Punch"].ToString();
            m_Jump.text = keyCodes["Jump"].ToString();
            m_Sprint.text = keyCodes["Sprint"].ToString();
            m_Pickup.text = keyCodes["Pickup"].ToString();
            m_Throw.text = keyCodes["Throw"].ToString();
            m_Ability1.text = keyCodes["Ability1"].ToString();
            m_Ability2.text = keyCodes["Ability2"].ToString();
            m_Ability3.text = keyCodes["Ability3"].ToString();
            m_Ability4.text = keyCodes["Ability4"].ToString();
        }

        // OnGUI so we can use Events to track which keys have been pressed
        //
        private void OnGUI()
        {
            if (m_CurrentKey == null)
                return;

            var keyPressed = KeyCode.None;
            var currentEvent = Event.current;

            if (currentEvent.isKey)
                keyPressed = currentEvent.keyCode;

            if (currentEvent.isMouse)
                keyPressed = GetButtonKeyCode(currentEvent.button);

            if (keyPressed == KeyCode.None)
                return;

            if (KeybindMap.KeyCodes.ContainsValue(keyPressed))
            {
                StartCoroutine(nameof(ErrorMessage));
                m_CurrentKey = null;
                return;
            }

            KeybindMap.KeyCodes[m_CurrentKey.name] = keyPressed;
            m_CurrentKey.GetComponentInChildren<Text>().text = currentEvent.isKey
                ? currentEvent.keyCode.ToString()
                : GetTextForMouseButton(keyPressed);

            m_CurrentKey = null;
        }

        /// <summary>
        /// Pass in the gameObject so we can use the name to check what key it is
        /// </summary>
        public void ChangeKey(GameObject go)
        {
            m_CurrentKey = go;
        }

        /// <summary>
        /// Save player key bindings using PlayerPrefs 
        /// </summary>
        public void SaveData()
        {
            foreach (var keyCode in KeybindMap.KeyCodes)
            {
                PlayerPrefs.SetString(keyCode.Key, keyCode.Value.ToString());
            }

            PlayerPrefs.Save();

            UpdateLabels();
        }

        /// <summary>
        /// Resets the settings to default
        /// </summary>
        public void DefaultSettings()
        {
            var keyCodes = KeybindMap.KeyCodes;

            keyCodes["Forward"] = KeyCode.W;
            keyCodes["Back"] = KeyCode.S;
            keyCodes["Left"] = KeyCode.A;
            keyCodes["Right"] = KeyCode.D;
            keyCodes["Punch"] = KeyCode.Mouse0;
            keyCodes["Jump"] = KeyCode.Space;
            keyCodes["Sprint"] = KeyCode.LeftShift;
            keyCodes["Pickup"] = KeyCode.E;
            keyCodes["Throw"] = KeyCode.Mouse1;
            keyCodes["Ability1"] = KeyCode.Alpha1;
            keyCodes["Ability2"] = KeyCode.Alpha2;
            keyCodes["Ability3"] = KeyCode.Alpha3;
            keyCodes["Ability4"] = KeyCode.Alpha4;

            PlayerPrefs.Save();

            UpdateLabels();
            m_Punch.text = GetTextForMouseButton(keyCodes["Punch"]);
            m_Throw.text = GetTextForMouseButton(keyCodes["Throw"]);
        }

        /// <summary>
        /// Temporarily display the error message
        /// </summary>
        private IEnumerator ErrorMessage()
        {
            m_ErrorMessage.ToggleGroup(true);

            yield return new WaitForSeconds(1F);

            m_ErrorMessage.ToggleGroup(false);
        }

        /// <summary>
        /// Convert button input to keycode
        /// </summary>
        private KeyCode GetButtonKeyCode(int button)
        {
            switch (button)
            {
                case 0:
                    return KeyCode.Mouse0;
                case 1:
                    return KeyCode.Mouse1;
                case 2:
                    return KeyCode.Mouse2;
                default:
                    return KeyCode.None;
            }
        }

        /// <summary>
        /// Modify text for mouse input from default numbering
        /// </summary>
        private string GetTextForMouseButton(KeyCode keycode)
        {
            switch (keycode)
            {
                case KeyCode.Mouse0:
                    return "LMB";
                case KeyCode.Mouse1:
                    return "RMB";
                case KeyCode.Mouse2:
                    return "MWheel";
                default:
                    return "UNKNOWN";
            }
        }
    }
}
