using System.Collections.Generic;
using UnityEngine;

namespace AI.Waypoints
{
    public class WaypointsHolder : MonoBehaviour
    {
        public IEnumerable<Waypoint> Waypoints { get; private set; }

        private void Start ()
        {
            Waypoints = GetComponentsInChildren<Waypoint>();
        }
    }
}
