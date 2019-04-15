using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class FireHydrant : MonoBehaviour
    {
        [SerializeField]
        private GameObject fireHydrant;
        [SerializeField]
        private GameObject brokenFireHydrant;
        [SerializeField]
        private ParticleSystem particleEffect;

        // Switch to broken fire hydrant
        // Instantiate particle effect
        // Destroy script so we don't try and access it again
        //
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                fireHydrant.SetActive(false);
                brokenFireHydrant.SetActive(true);

                Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(GetComponent<FireHydrant>());
            }
        }
    }
}
