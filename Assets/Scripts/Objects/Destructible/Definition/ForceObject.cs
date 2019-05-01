using UnityEngine;

namespace Objects.Destructible.Definition
{
    internal abstract class ForceObject : MonoBehaviour
    {
        [SerializeField]
        [Range(100, 500)]
        protected float force = 150;

        protected Rigidbody Rb => GetComponent<Rigidbody>();

        /// <summary>
        /// Calculates the opposite direction of the given collider,
        /// and adds force to this RigidBody in that direction
        /// </summary>
        protected void AddForce(Collider col, Rigidbody rb)
        {
            var direction = (transform.position - col.gameObject.transform.position).normalized;

            rb.AddForce(direction * force);
        }
    }
}
