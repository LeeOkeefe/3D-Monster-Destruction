using Extensions;
using UnityEngine;

namespace UI.Menu
{
    internal sealed class ControlsManager : MonoBehaviour
    {
        private CanvasGroup m_ControlsCanvasGroup;

        private void Start ()
        {
            m_ControlsCanvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Toggles the controls canvasGroup options to true
        /// </summary>
        public void OpenControls()
        {
            m_ControlsCanvasGroup.ToggleGroup(true);
        }

        /// <summary>
        /// Toggles the controls canvasGroup options to false
        /// </summary>
        public void CloseControls()
        {
            m_ControlsCanvasGroup.ToggleGroup(false);
        }
    }
}
