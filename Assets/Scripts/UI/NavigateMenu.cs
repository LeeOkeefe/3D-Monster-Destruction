using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    internal sealed class NavigateMenu
    {
        /// <summary>
        /// Loads scene "Level 1" to begin the game
        /// </summary>
        public void StartGame()
        {
            if (Math.Abs(Time.timeScale) < 0)
            {
                Time.timeScale = 1;
            }

            SceneManager.LoadScene("Level 1");
        }

        /// <summary>
        /// Checks the active scene and reloads it
        /// </summary>
        public void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();

            if (scene.name != "Level 1" || !scene.IsValid())
            {
                Debug.Log($"Scene loaded: {scene.name} + " +
                          $"Scene IsValid: {scene.IsValid()}");
            }

            SceneManager.LoadSceneAsync(scene.name);
        }

        /// <summary>
        /// Quits the game using <see cref="Application.Quit"/>
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Returns to the main menu by loading the scene
        /// </summary>
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
        }

        /// <summary>
        /// Pauses the game using <see cref="Time.timeScale"/>
        /// </summary>
        public void PauseGame()
        {
            if (Math.Abs(Time.timeScale) < 0)
            {
                throw new Exception("The game is already paused.");
            }

            Time.timeScale = 0;
        }

        /// <summary>
        /// Resumes the game by setting <see cref="Time.timeScale"/> to 1
        /// </summary>
        public void ResumeGame()
        {
            if (Math.Abs(Time.timeScale - 1) < 0)
            {
                throw new Exception("The game is already running");
            }

            Time.timeScale = 1;
        }
    }
}