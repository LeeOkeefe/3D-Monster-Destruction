using System;
using UnityEngine;
using System.Collections.Generic;

internal sealed class CarAI : MonoBehaviour
{
    [SerializeField]
    private Transform waypointsParent;
    [SerializeField]
    private int startWaypointIndex;

    [SerializeField]
    private float acceleration = 1.5f;
    [SerializeField]
    private float deceleration = 2.5f;
    [SerializeField]
    private float maxMoveSpeed = 5.0f;
    [SerializeField]
    private float rotationSpeed = 5.0f;

    [SerializeField]
    private Transform[] wheels;

    private Transform m_Transform;

    private readonly IList<Transform> m_Waypoints = new List<Transform>();

    private int m_WaypointIndex;
    private int m_LastWaypointId;
    private float m_CurrentSpeed;
    private bool m_Accelerating = true;

    private Junction m_CurrentJunction;

    private const int MinMoveSpeed = 0;

    private void Start()
    {
        if (waypointsParent == null)
        {
            throw new NullReferenceException("waypointsParent was null.");
        }

        m_Transform = GetComponent<Transform>();

        foreach (Transform child in waypointsParent)
        {
            m_Waypoints.Add(child);
        }

        if (startWaypointIndex < m_Waypoints.Count)
        {
            m_WaypointIndex = startWaypointIndex;
        }
    }

    private void Update()
    {
        Movement();
    }

    /// <summary>
    /// Handles the movement speed and calls Rotation
    /// </summary>
    private void Movement()
    {
        if (m_Accelerating)
        {
            m_CurrentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            m_CurrentSpeed -= deceleration * Time.deltaTime;
        }

        m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, MinMoveSpeed, maxMoveSpeed);
        m_Transform.Translate(0, 0, m_CurrentSpeed * Time.deltaTime);

        Rotation();
    }

    /// <summary>
    /// Set the rotation of the vehicle towards the next waypoint (excludes Y axis),
    /// and rotate the wheels
    /// </summary>
    private void Rotation()
    {
        var direction = (m_Waypoints[m_WaypointIndex].position - m_Transform.position).normalized;

        direction = new Vector3(direction.x, 0.0f, direction.z);

        var newRotation = Quaternion.LookRotation(direction);

        m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

        foreach (var wheel in wheels)
        {
            wheel.Rotate(Vector3.right, m_CurrentSpeed * Time.deltaTime * 90, Space.Self);
        }
    }

    // Check waypoint and distance, ensure we don't set it to the previous waypoint
    // set the waypoint to the next one, and start accelerating
    //
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Waypoint") && Vector3.Distance(m_Transform.position, m_Waypoints[m_WaypointIndex].position) < 1)
        {
            if (other.GetInstanceID() == m_LastWaypointId)
                return;

            m_WaypointIndex++;

            if (m_WaypointIndex >= m_Waypoints.Count)
            {
                m_WaypointIndex = 0;
            }

            m_LastWaypointId = other.GetInstanceID();
        }

        if (!m_Accelerating && other.CompareTag("Junction") && m_CurrentJunction.free)
        {
            m_Accelerating = true;
        }
    }

    // Check we have hit the junction and stop accelerating
    // unless the junction is "free"
    //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Junction"))
        {
            m_CurrentJunction = other.GetComponent<Junction>();

            if (m_CurrentJunction.free)
                return;

            m_Accelerating = false;
        }
    }

    // Continue to accelerate if we haven't hit the junction collider
    //
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Junction"))
            return;

        m_Accelerating = true;
    }
}
