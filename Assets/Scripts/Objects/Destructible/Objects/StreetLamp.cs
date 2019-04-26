using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class StreetLamp : MonoBehaviour
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
            }
        }
    }
}
