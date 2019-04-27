using UnityEngine;

namespace AI.Traffic_System
{
    internal sealed class JunctionController : MonoBehaviour
    {
        [SerializeField]
        private Junction[] junctions;

        private const float GreenLightTime = 7.5F;
        private const float AmberLightTime = 2;

        private float m_Timer;
        private int m_JunctionIndex;
        private bool m_IsWaiting;

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (!m_IsWaiting && m_Timer >= GreenLightTime)
            {
                GreenLight();
            }

            if (m_IsWaiting && m_Timer >= GreenLightTime + AmberLightTime)
            {
                AmberLight();
            }
        }

        /// <summary>
        /// Set the state of the traffic lights green light
        /// </summary>
        private void GreenLight()
        {
            junctions[m_JunctionIndex].SetTrafficState(false, true);

            if (m_JunctionIndex == junctions.Length - 1)
            {
                m_JunctionIndex = 0;
            }
            else
            {
                m_JunctionIndex++;
            }

            junctions[m_JunctionIndex].SetTrafficState(false, true);
            m_IsWaiting = true;
        }

        /// <summary>
        /// Set the state of the traffic lights amber light
        /// </summary>
        private void AmberLight()
        {
            if (m_JunctionIndex == 0)
            {
                junctions[junctions.Length - 1].SetTrafficState(false, false);
            }
            else
            {
                junctions[m_JunctionIndex - 1].SetTrafficState(false, false);
            }

            junctions[m_JunctionIndex].SetTrafficState(true, false);

            m_IsWaiting = false;
            m_Timer = 0;
        }
    }
}
