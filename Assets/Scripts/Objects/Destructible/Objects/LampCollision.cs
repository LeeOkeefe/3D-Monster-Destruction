using System.Security.Cryptography;
using AI.Traffic_System;
using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class LampCollision : MonoBehaviour
    {
        [SerializeField]
        [Range(150, 300)]
        private float force = 150;

        private Rigidbody m_Rb;

        private void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
        }

        // Use AddForce to push rigidBody over
        // Check if it's a traffic light, and deactivate the light system if it is
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            var direction = (transform.position - other.gameObject.transform.position).normalized;

            m_Rb.AddForce(direction * force);

            if (!gameObject.CompareTag("TrafficLight"))
                return;

            DeactivateTrafficLight();
        }

        /// <summary>
        /// Resets Lights to be grey, then deletes the scripts
        /// </summary>
        private void DeactivateTrafficLight()
        {
            var junction = GetComponentInChildren<Junction>();
            junction.ResetLights();
            Destroy(junction);
            Destroy(this);
        }
    }
}
