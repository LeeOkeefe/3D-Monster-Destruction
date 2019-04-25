using UnityEngine;

namespace Traffic_System
{
    internal sealed class Waypoint : MonoBehaviour
    {
        private int m_DrawGizmoRadius = 1;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_DrawGizmoRadius);
        }
    }
}
