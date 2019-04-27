using Objects.Destructible.Definition;
using UnityEngine;

namespace Objects.Destructible.Objects.ForceObjects
{
    internal sealed class ForceStreetLight : ForceObject
    {
        // Use AddForce to push rigidBody over
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            AddForce(other);
        }
    }
}
