using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleMouse(bool show)
    {
        Cursor.lockState = show
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = show;
    }
}
