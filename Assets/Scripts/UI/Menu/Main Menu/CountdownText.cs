using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu.Main_Menu
{
    internal sealed class CountdownText : MonoBehaviour
    {
        private float m_CurrentTime;
        private const float StartingTime = 10f;
        private Text m_CountdownText;
        public bool startCountDownTimer;

        private void Start()
        {
            m_CountdownText = GetComponent<Text>();
            m_CurrentTime = StartingTime;
        }

        private void Update()
        {
            if (startCountDownTimer)
            {
                BeginCountDownTimer();
            }
        }

        /// <summary>
        /// Begins counting down the timer and displaying it in the Text format
        /// </summary>
        private void BeginCountDownTimer()
        {
            m_CurrentTime -= 1 * Time.deltaTime;
            m_CountdownText.text = m_CurrentTime.ToString("0");

            if (m_CurrentTime <= 0)
            {
                m_CurrentTime = 0;
                SceneManager.LoadScene("Level 1");
            }
        }

        /// <summary>
        /// Boolean we can set to true via the "Play Button"
        /// </summary>
        public void StartTimer()
        {
            startCountDownTimer = true;
        }
    }
}
