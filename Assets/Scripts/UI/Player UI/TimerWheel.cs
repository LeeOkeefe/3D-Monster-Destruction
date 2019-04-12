using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player_UI
{
    internal sealed class TimerWheel : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private CanvasGroup labelBackground;
        [SerializeField]
        private Text label;

        private float m_Duration;
        private float m_TimeLeft;
        private Action m_OnFinishAction;
        private bool m_IsActive;

        private void Update()
        {
            if (!m_IsActive)
            {
                return;
            }

            m_TimeLeft -= Time.deltaTime;
            image.fillAmount -= 1.0f / m_Duration * Time.deltaTime;
            labelBackground.alpha = 1;
            label.text = $"{Mathf.RoundToInt(m_TimeLeft)}";

            if (m_TimeLeft <= 0)
            {
                m_OnFinishAction();
                m_IsActive = false;
                label.text = string.Empty;
                labelBackground.alpha = 0;
            }
        }

        /// <summary>
        /// Initialize the TimerWheel properties
        /// </summary>
        public void Initialize(float duration, Action action)
        {
            image.fillAmount = 1;
            m_Duration = duration;
            m_TimeLeft = duration;
            m_IsActive = true;
            m_OnFinishAction = action;
        }
    }
}
