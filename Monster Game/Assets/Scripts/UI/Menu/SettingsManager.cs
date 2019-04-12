using Extensions;
using UnityEngine;

namespace UI.Menu
{
    internal sealed class SettingsManager : MonoBehaviour
    {
        private NavigateMenu m_Navigate;

        [SerializeField]
        private CanvasGroup settingsCanvasGroup;

        private bool m_SettingsOpen;

        private void Start()
        {
            m_Navigate = new NavigateMenu();
        }

        /// <summary>
        /// Toggle between pause states
        /// </summary>
        public void ToggleSettings()
        {
            if (!m_SettingsOpen)
            {
                OpenSettings();
            }
            else
            {
                CloseSettings();
            }
        }

        /// <summary>
        /// enable settings UI canvas group
        /// </summary>
        public void OpenSettings()
        {
            m_Navigate.PauseGame();
            settingsCanvasGroup.ToggleGroup(true);
            m_SettingsOpen = true;
        }

        /// <summary>
        /// Disable settings UI canvas group
        /// </summary>
        public void CloseSettings()
        {
            m_Navigate.ResumeGame();
            settingsCanvasGroup.ToggleGroup(false);
            m_SettingsOpen = false;
        }
    }
}
