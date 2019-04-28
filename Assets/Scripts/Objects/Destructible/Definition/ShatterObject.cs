using System.Collections.Generic;
using UnityEngine;

namespace Objects.Destructible.Definition
{
    internal sealed class ShatterObject : MonoBehaviour, IShatter
    {
        [SerializeField]
        [Range(0, 50)]
        private float explosiveForce;
        [SerializeField]
        [Range(0, 50)]
        private float explosiveRadius;

        [SerializeField]
        private GameObject fragmentsParent;
        [SerializeField]
        private float scoreAwarded;

        private IEnumerable<Rigidbody> m_Fragments;

        private void Start()
        {
            m_Fragments = GetComponentsInChildren<Rigidbody>();
        }

        /// <summary>
        /// Adds explosive force to each fragment 
        /// </summary>
        public void Explode(float force, Vector3 direction, float radius)
        {
            fragmentsParent.SetActive(true);
            transform.DetachChildren();
            Destroy(gameObject);

            foreach (var fragment in m_Fragments)
            {
                fragment.GetComponent<Rigidbody>().AddExplosionForce(force, direction, radius);
            }
        }

        // Explode fence in the opposite direction to the other collider
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Right Foot") && !other.gameObject.CompareTag("Left Foot"))
                return;

            var direction = (transform.position - other.gameObject.transform.position).normalized;

            Explode(explosiveForce, direction, explosiveRadius);
            ScoreManager.AddScore(scoreAwarded);
        }
    }
}
