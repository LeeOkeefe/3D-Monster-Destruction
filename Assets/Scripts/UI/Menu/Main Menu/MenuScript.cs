using Extensions;
using UnityEngine;

namespace UI.Menu.Main_Menu
{
    internal sealed class MenuScript : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup quitMenu;
        [SerializeField]
        private CanvasGroup startButton;
        [SerializeField]
        private CanvasGroup options;
        [SerializeField]
        private CanvasGroup exitButton;
        [SerializeField]
        private CanvasGroup highScores;

        private NavigateMenu m_Navigation;

        public void StartGame() => m_Navigation.StartGame();
        public void QuitGame() => m_Navigation.QuitGame();

        private void Start()
        {
            m_Navigation = new NavigateMenu();
        }

        /// <summary>
        /// Opens option to confirm whether the player wants to quit,
        /// disables other buttons whilst active.
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Opens the high scores page
        /// </summary>
        public void OpenHighScores()
        {
            startButton.interactable = false;
            options.interactable = false;
            exitButton.interactable = false;
            highScores.ToggleGroup(true);
        }

        /// <summary>
        /// Closes the high scores page
        /// </summary>
        public void CloseHighScores()
        {
            startButton.interactable = true;
            options.interactable = true;
            exitButton.interactable = true;
            highScores.ToggleGroup(false);
        }
    }
}