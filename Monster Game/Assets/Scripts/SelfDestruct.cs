using UnityEngine;

internal sealed class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 1.5f;

    private float m_Timer;

    // Destroy gameObject after 1.5 seconds
    private void Update ()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
