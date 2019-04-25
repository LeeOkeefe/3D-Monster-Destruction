using System;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic_System
{
    internal abstract class CarFunctionality : MonoBehaviour
    {
        [SerializeField]
        private float acceleration = 1.5f;
        [SerializeField]
        private float deceleration = 2.5f;
        [SerializeField]
        private float maxMoveSpeed = 5.0f;
        [SerializeField]
        private float rotationSpeed = 5.0f;

        private float m_CurrentSpeed;
        private const int MinMoveSpeed = 0;

        [SerializeField]
        private Transform[] wheels;
        [SerializeField]
        private  Transform waypointsParent;
        [SerializeField]
        private int startWaypointIndex;

        protected Transform Transform;
        protected bool Accelerating = true;
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
        /// Handles the movement speed and calls Rotation
        /// </summary>
        protected void Movement()
        {
            if (Accelerating)
            {
                m_CurrentSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                m_CurrentSpeed -= deceleration * Time.deltaTime;
            }

            m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, MinMoveSpeed, maxMoveSpeed);
            Transform.Translate(0, 0, m_CurrentSpeed * Time.deltaTime);

            Rotation();
        }

        /// <summary>
        /// Set the rotation of the vehicle towards the next waypoint (excludes Y axis),
        /// and rotate the wheels
        /// </summary>
        private void Rotation()
        {
            var direction = (m_Waypoints[WaypointIndex].position - Transform.position).normalized;

            direction = new Vector3(direction.x, 0.0f, direction.z);

            var newRotation = Quaternion.LookRotation(direction);

            Transform.rotation = Quaternion.Slerp(Transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

            foreach (var wheel in wheels)
            {
                wheel.Rotate(Vector3.right, m_CurrentSpeed * Time.deltaTime * 90, Space.Self);
            }
        }
    }
}
