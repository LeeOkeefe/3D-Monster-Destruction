using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Waypoints
{
    internal abstract class WaypointAI : MonoBehaviour
    {
        [SerializeField]
        private float acceleration = 1.5f;
        [SerializeField]
        private float deceleration = 2.5f;
        [SerializeField]
        private float maxMoveSpeed = 5.0f;
        [SerializeField]
        protected float rotationSpeed = 5.0f;

        protected float CurrentSpeed;
        private const int MinMoveSpeed = 0;

        [SerializeField]
        private Transform waypointsParent;
        [SerializeField]
        private int startWaypointIndex;

        protected Transform Transform;
        protected bool Accelerating = true;
        protected int m_LastWaypointId;
        protected int WaypointIndex;

        protected readonly IList<Transform> m_Waypoints = new List<Transform>();

        /// <summary>
        /// Get all positions of waypoints & set starting waypoint
        /// </summary>
        protected void Initialize()
        {
            if (waypointsParent == null)
            {
                throw new NullReferenceException("waypointsParent was null.");
            }

            Transform = GetComponent<Transform>();

            foreach (Transform child in waypointsParent)
            {
                m_Waypoints.Add(child);
            }

            if (startWaypointIndex < m_Waypoints.Count)
            {
                WaypointIndex = startWaypointIndex;
            }
        }

        /// <summary>
        /// Handles the movement and rotation of the gameObject
        /// </summary>
        protected void Movement()
        {
            if (Accelerating)
            {
                CurrentSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                CurrentSpeed -= deceleration * Time.deltaTime;
            }

            CurrentSpeed = Mathf.Clamp(CurrentSpeed, MinMoveSpeed, maxMoveSpeed);
            Transform.Translate(0, 0, CurrentSpeed * Time.deltaTime);
            Rotation();
        }

        /// <summary>
        /// Set the rotation of the transform towards the next waypoint (excludes Y axis),
        /// </summary>
        protected virtual void Rotation()
        {
            var direction = (m_Waypoints[WaypointIndex].position - Transform.position).normalized;

            direction = new Vector3(direction.x, 0.0f, direction.z);

            var newRotation = Quaternion.LookRotation(direction);

            Transform.rotation = Quaternion.Slerp(Transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
