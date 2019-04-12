using Extensions;
using UnityEngine;

namespace UI.Menu
{
    internal sealed class PauseMenu : MonoBehaviour
    {
        private CanvasGroup m_PauseMenu;

        private bool m_IsGamePaused;

        private void Start()
        {
            m_PauseMenu = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Toggle between pause states
        /// </summary>
        public void ToggleMenu()
        {
            if (m_IsGamePaused == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        /// <summary>
        /// Pauses the game by setting the <see cref="Time.timeScale"/> to 0
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0f;
            m_PauseMenu.ToggleGroup(true);
            m_IsGamePaused = true;
        }

        /// <summary>
        /// Resumes the game by setting the <see cref="Time.timeScale"/> to 1
        /// </summary>
        public void ResumeGame()
        {
            Time.timeScale = 1f;
            m_PauseMenu.ToggleGroup(false);
            m_IsGamePaused = false;
        }

        /// <summary>
        /// Quits the game using <see cref="Application"/>
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
