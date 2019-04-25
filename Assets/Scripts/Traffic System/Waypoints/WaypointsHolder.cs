using System.Collections.Generic;
using UnityEngine;

namespace Traffic_System
{
    internal sealed class WaypointsHolder : MonoBehaviour
    {
        public IList<Waypoint> m_Waypoints { get; private set; }

        private void Start()
        {
            m_Waypoints = GetComponentsInChildren<Waypoint>();
        }
    }
}
