using Extensions;
using UnityEngine;

namespace UI.Menu
{
    internal sealed class ControlsManager : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup resetConfirmation;

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

        /// <summary>
        /// Open reset confirmation UI
        /// </summary>
        public void OpenConfirmation()
        {
            resetConfirmation.ToggleGroup(true);
        }

        /// <summary>
        /// Close confirmation UI
        /// </summary>
        public void CloseConfirmation()
        {
            resetConfirmation.ToggleGroup(false);
        }
    }
}
