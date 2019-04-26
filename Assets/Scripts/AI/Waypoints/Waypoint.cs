using UnityEngine;

namespace AI.Waypoints
{
    internal sealed class Waypoint : WaypointsHolder
    {
        [SerializeField]
        private float gizmoDrawRadius = 1;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, gizmoDrawRadius);
        }
    }
}
