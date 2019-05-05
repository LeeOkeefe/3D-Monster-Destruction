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

        public Dictionary<string, KeyCode> m_KeyCodes;

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

            m_KeyCodes = new Dictionary<string, KeyCode>
            {
                { "Forward", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"))},
                { "Back", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Back", "S"))},
                { "Left", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"))},
                { "Right", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"))},
                { "Punch", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Punch", "Mouse0"))},
                { "Jump", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"))},
                { "Sprint", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"))},
                { "Pickup", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pickup", "E"))},
                { "Throw", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Throw", "Mouse1"))},
                { "Ability1", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ability1", "Alpha1"))},
                { "Ability2", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ability2", "Alpha2"))},
                { "Ability3", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ability3", "Alpha3"))},
                { "Ability4", (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Ability4", "Alpha4"))}
            };

            UpdateLabels();
        }

        /// <summary>
        /// Update Text components of the custom input buttons
        /// </summary>
        public void UpdateLabels()
        {
            m_Forward.text = m_KeyCodes["Forward"].ToString();
            m_Back.text = m_KeyCodes["Back"].ToString();
            m_Left.text = m_KeyCodes["Left"].ToString();
            m_Right.text = m_KeyCodes["Right"].ToString();
            m_Punch.text = m_KeyCodes["Punch"].ToString();
            m_Jump.text = m_KeyCodes["Jump"].ToString();
            m_Sprint.text = m_KeyCodes["Sprint"].ToString();
            m_Pickup.text = m_KeyCodes["Pickup"].ToString();
            m_Throw.text = m_KeyCodes["Throw"].ToString();
            m_Ability1.text = m_KeyCodes["Ability1"].ToString();
            m_Ability2.text = m_KeyCodes["Ability2"].ToString();
            m_Ability3.text = m_KeyCodes["Ability3"].ToString();
            m_Ability4.text = m_KeyCodes["Ability4"].ToString();
        }

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

            if (m_KeyCodes.ContainsValue(keyPressed))
            {
                StartCoroutine(nameof(ErrorMessage));
                m_CurrentKey = null;
                return;
            }

            m_KeyCodes[m_CurrentKey.name] = keyPressed;
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
            foreach (var keyCode in m_KeyCodes)
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
            m_KeyCodes["Forward"] = KeyCode.W;
            m_KeyCodes["Back"] = KeyCode.S;
            m_KeyCodes["Left"] = KeyCode.A;
            m_KeyCodes["Right"] = KeyCode.D;
            m_KeyCodes["Punch"] = KeyCode.Mouse0;
            m_KeyCodes["Jump"] = KeyCode.Space;
            m_KeyCodes["Sprint"] = KeyCode.LeftShift;
            m_KeyCodes["Pickup"] = KeyCode.E;
            m_KeyCodes["Throw"] = KeyCode.Mouse1;
            m_KeyCodes["Ability1"] = KeyCode.Alpha1;
            m_KeyCodes["Ability2"] = KeyCode.Alpha2;
            m_KeyCodes["Ability3"] = KeyCode.Alpha3;
            m_KeyCodes["Ability4"] = KeyCode.Alpha4;

            PlayerPrefs.Save();

            UpdateLabels();
            m_Punch.text = GetTextForMouseButton(m_KeyCodes["Punch"]);
            m_Throw.text = GetTextForMouseButton(m_KeyCodes["Throw"]);
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
