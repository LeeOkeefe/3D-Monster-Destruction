using AI.Waypoints;
using UnityEngine;

namespace AI.Traffic_System
{
    internal sealed class CarAI : WaypointAI
    {
        [SerializeField]
        private Transform[] wheels;
        [SerializeField]
        public float stoppingDistance = 5f;
        [SerializeField]
        private LayerMask decelerationLayer;

        private Junction m_CurrentJunction;
        private bool m_OnJunction;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Movement();
        }

        // Check if we hit are hitting any layer masks so we can stop 
        // used sphere and ray because it's far more accurate, but probably expensive & a better way to do it
        //
        private void FixedUpdate()
        {
            if (m_OnJunction)
                return;

            RaycastHit hit;
            
            Accelerating = !Physics.SphereCast(Transform.position, 2F, Transform.forward, out hit, stoppingDistance, decelerationLayer);
            Accelerating = !Physics.Raycast(Transform.position, Transform.forward, out hit, stoppingDistance, decelerationLayer);
        }

        // Override rotation because we need to handle the wheel rotation for vehicles
        //
        protected override void Rotation()
        {
            var direction = (m_Waypoints[WaypointIndex].position - Transform.position).normalized;

            direction = new Vector3(direction.x, 0.0f, direction.z);

            var newRotation = Quaternion.LookRotation(direction);

            Transform.rotation = Quaternion.Slerp(Transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

            foreach (var wheel in wheels)
            {
                wheel.Rotate(Vector3.right, CurrentSpeed * Time.deltaTime * 90, Space.Self);
            }
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
