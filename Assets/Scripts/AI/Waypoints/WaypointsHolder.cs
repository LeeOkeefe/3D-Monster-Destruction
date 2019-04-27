using System.Collections.Generic;
using UnityEngine;

namespace AI.Waypoints
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
