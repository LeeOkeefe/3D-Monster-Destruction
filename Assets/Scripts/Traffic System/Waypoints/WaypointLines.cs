using System;
using System.Collections.Generic;
using Traffic_System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointsHolder))]
internal sealed class WaypointLines : Editor
{
    private IList<Transform> m_Waypoints;

    private void OnSceneGUI()
    {
        var waypointsHolder = target as WaypointsHolder;

        if (waypointsHolder == null)
        {
            throw new NullReferenceException("Waypoints Holder was null.");
        }

        m_Waypoints = waypointsHolder.GetComponent<Waypoint>() ? 
            waypointsHolder.transform.parent.GetComponentsInChildren<Transform>() : waypointsHolder.GetComponentsInChildren<Transform>();

        Handles.color = Color.cyan;

        for (var i = 1; i < m_Waypoints.Count - 1; i++)
        {
            Handles.DrawLine(m_Waypoints[i].position, m_Waypoints[i + 1].position);
        }
    }
}