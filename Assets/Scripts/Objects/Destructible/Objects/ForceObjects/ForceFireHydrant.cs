using Objectives;
using Objects.Destructible.Definition;
using UnityEngine;

namespace Objects.Destructible.Objects.ForceObjects
{
    internal sealed class ForceFireHydrant : ForceObject
    {
        [SerializeField]
        private ParticleSystem particleEffect;

        // Adds force to the gameObject
        // Instantiates the particle effect
        // Destroy this script after, so we only execute the code once
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            AddForce(other, Rb);
            Instantiate(particleEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(this);

            ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.FireHydrant);
        }
    }
}
