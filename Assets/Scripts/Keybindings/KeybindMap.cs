using System;
using System.Collections.Generic;
using UnityEngine;

namespace Keybindings
{
    internal static class KeybindMap
    {
        // Static dictionary that stores all our keybindings, 
        // which can be accessed from anywhere
        //
        public static Dictionary<string, KeyCode> KeyCodes = new Dictionary<string, KeyCode>
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
    }
}
