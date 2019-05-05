using Objectives;
using Objects.Destructible.Definition;
using UnityEngine;

namespace Objects.Destructible.Objects.ForceObjects
{
    internal sealed class ForceStreetLight : ForceObject
    {
        private bool m_Objective;

        // Use AddForce to push rigidBody over
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            AddForce(other, Rb);

            if (!m_Objective)
            {
                ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.StreetLamp);
                m_Objective = true;
            }
        }
    }
}
