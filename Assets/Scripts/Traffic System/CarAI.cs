using UnityEngine;

namespace Traffic_System
{
    internal sealed class CarAI : CarFunctionality
    {
        [SerializeField]
        public float distanceBeforeDeceleration = 5f;
        [SerializeField]
        private LayerMask decelerationLayer;

        private Junction m_CurrentJunction;
        private int m_LastWaypointId;
        private bool m_OnJunction;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Movement();
        }

        // Send out raycast, check if we hit are hitting layermask
        // When we stop hitting layer (car), start moving again
        //
        private void FixedUpdate()
        {
            if (m_OnJunction)
                return;

            RaycastHit hit;

            Accelerating = !Physics.Raycast(Transform.position, Transform.forward * 5, 
                                              out hit, distanceBeforeDeceleration, decelerationLayer);
        }

        // Check waypoint and distance, ensure we don't set it to the previous waypoint
        // set the waypoint to the next one, and start accelerating
        //
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Waypoint") && Vector3.Distance(Transform.position, m_Waypoints[WaypointIndex].position) < 1)
            {
                if (other.GetInstanceID() == m_LastWaypointId)
                    return;

                WaypointIndex++;

                if (WaypointIndex >= m_Waypoints.Count)
                {
                    WaypointIndex = 0;
                }

                m_LastWaypointId = other.GetInstanceID();
            }

            if (Accelerating || !other.CompareTag("Junction") || !m_CurrentJunction.Free)
                return;

            m_OnJunction = false;
            Accelerating = true;
        }

        // Check we have hit the junction and stop accelerating
        // unless the junction is "free"
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Junction"))
                return;

            m_CurrentJunction = other.GetComponent<Junction>();

            if (m_CurrentJunction.Free)
                return;

            m_OnJunction = true;
            Accelerating = false;
        }

        // Continue to accelerate if we haven't hit the junction collider
        //
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Junction"))
                return;

            m_OnJunction = false;
            Accelerating = true;
        }
    }
}
