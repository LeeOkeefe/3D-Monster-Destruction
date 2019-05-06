using Objects.Destructible.Definition;
using UI.Settings.Audio;
using UnityEngine;

namespace Objects.Destructible.Objects.ForceObjects
{
    internal sealed class ForceMetalFence : ForceObject
    {
        [SerializeField]
        private GameObject brokenFence;
        [SerializeField]
        private Rigidbody brokenFenceRb;
        [SerializeField]
        private AudioClip audioClip;

        // Set broken fence active and add force
        // Detach the broken fence child object and destroy parent
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            SoundEffectManager.Instance.PlayClipAtPoint(audioClip, transform.position);
            brokenFence.SetActive(true);
            AddForce(other, brokenFenceRb);

            brokenFence.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
