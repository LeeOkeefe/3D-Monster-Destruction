using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal sealed class GameOver : MonoBehaviour
    {
        private NavigateMenu m_Navigate;
        private CanvasGroup m_CanvasGroup;

        [SerializeField]
        private Button pauseButton;

        public void MainMenu() => m_Navigate.ReturnToMainMenu();
        public void QuitGame() => m_Navigate.QuitGame();

        private void Start()
        {
            m_Navigate = new NavigateMenu();
            m_CanvasGroup = GetComponent<CanvasGroup>();
            Time.timeScale = 1;
        }

        /// <summary>
        /// Restart the current scene
        /// </summary>
        public void PlayAgain()
        {
            m_Navigate.RestartGame();
            m_CanvasGroup.ToggleGroup(false);
            pauseButton.enabled = true;
        }
    }
}
