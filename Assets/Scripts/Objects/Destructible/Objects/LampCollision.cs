using Traffic_System;
using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class LampCollision : MonoBehaviour
    {
        private bool m_Knocked;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            if (m_Knocked)
            {
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            }
            else
            {
                m_Knocked = true;
                var junction = GetComponentInChildren<Junction>();
                junction.ResetLights();
                Destroy(junction.gameObject);
            }
        }
    }
}
