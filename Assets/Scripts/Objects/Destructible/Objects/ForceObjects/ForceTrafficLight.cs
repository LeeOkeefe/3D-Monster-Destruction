using AI.Traffic_System;
using Objects.Destructible.Definition;
using UnityEngine;

namespace Objects.Destructible.Objects.ForceObjects
{
    internal class ForceTrafficLight : ForceObject
    {
        // Use AddForce to push rigidBody over
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            AddForce(other, Rb);

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
