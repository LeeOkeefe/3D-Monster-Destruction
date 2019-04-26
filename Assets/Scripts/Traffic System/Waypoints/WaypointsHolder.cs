using System.Collections.Generic;
using UnityEngine;

namespace Traffic_System.Waypoints
{
    internal class WaypointsHolder : MonoBehaviour
    {
        public IEnumerable<Waypoint> Waypoints { get; private set; }

        private void Start ()
        {
            Waypoints = GetComponentsInChildren<Waypoint>();
        }
    }
}
