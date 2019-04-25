using UnityEngine;

internal sealed class JunctionController : MonoBehaviour
{
    [SerializeField]
	private Junction[] junctions;

    private const float GreenLightTime = 7.5F;
    private const float AmberLightTime = 2;

    private float m_Timer;
	private int m_JunctionIndex;
	private bool m_IsWaiting;

	private void Update ()
	{
		m_Timer += Time.deltaTime;

        GreenLight();
        AmberLight();
	}

    private void GreenLight()
    {
        if (!m_IsWaiting && m_Timer >= GreenLightTime)
        {
            junctions[m_JunctionIndex].free = false;
            junctions[m_JunctionIndex].waiting = true;

            if (m_JunctionIndex == junctions.Length - 1)
            {
                m_JunctionIndex = 0;
            }
            else
            {
                m_JunctionIndex++;
            }

            junctions[m_JunctionIndex].waiting = true;
            m_IsWaiting = true;
        }
    }

    private void AmberLight()
    {
        if (m_IsWaiting && m_Timer >= GreenLightTime + AmberLightTime)
        {
            if (m_JunctionIndex == 0)
            {
                junctions[junctions.Length - 1].waiting = false;
            }
            else
            {
                junctions[m_JunctionIndex - 1].waiting = false;
            }

            junctions[m_JunctionIndex].waiting = false;
            junctions[m_JunctionIndex].free = true;

            m_IsWaiting = false;
            m_Timer = 0;
        }
	}
}
