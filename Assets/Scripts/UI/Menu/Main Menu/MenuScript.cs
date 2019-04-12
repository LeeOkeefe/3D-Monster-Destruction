using Extensions;
using UnityEngine;

namespace UI.Menu.Main_Menu
{
    internal sealed class MenuScript : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup quitMenu;
        [SerializeField]
        private CanvasGroup monsterSelection;
        [SerializeField]
        private CanvasGroup startButton;
        [SerializeField]
        private CanvasGroup options;
        [SerializeField]
        private CanvasGroup exitButton;

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
        public void ExitMenu()
        {
            quitMenu.ToggleGroup(true);
            monsterSelection.ToggleGroup(false);
            startButton.interactable = false;
            options.interactable = false;
            exitButton.interactable = false;
        }

        /// <summary>
        /// Disables the confirmation text and enables the
        /// other main menu options/interactions again
        /// </summary>
        public void CancelExitGame()
        {
            startButton.interactable = true;
            options.interactable = true;
            exitButton.interactable = true;
            monsterSelection.ToggleGroup(false);
            quitMenu.ToggleGroup(false);
        }

        /// <summary>
        /// Enables the character selection page,
        /// and disables other menu canvas groups.
        /// </summary>
        public void OpenCharacterSelection()
        {
            startButton.ToggleGroup(false);
            options.ToggleGroup(false);
            exitButton.ToggleGroup(false);
            monsterSelection.ToggleGroup(true);
            quitMenu.ToggleGroup(false);
        }
    }
}