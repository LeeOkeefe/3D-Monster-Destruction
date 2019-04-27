using AI.Traffic_System;
using UnityEngine;

namespace Objects.Destructible.Definition
{
    internal abstract class ForceObject : MonoBehaviour
    {
        [SerializeField]
        [Range(150, 300)]
        protected float force = 150;

        protected Rigidbody Rb => GetComponent<Rigidbody>();

        /// <summary>
        /// Calculates the opposite direction of the given collider,
        /// and adds force to this RigidBody in that direction
        /// </summary>
        protected void AddForce(Collider col)
        {
            var direction = (transform.position - col.gameObject.transform.position).normalized;

            Rb.AddForce(direction * force);
        }
    }
}
