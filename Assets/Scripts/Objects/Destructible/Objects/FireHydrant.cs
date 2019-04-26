using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class FireHydrant : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particleEffect;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            var rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            Instantiate(particleEffect, transform.position, Quaternion.Euler(-90, 0, 0));

            Destroy(gameObject.GetComponent<Collider>(), 2);
            Destroy(this);
        }
    }
}
