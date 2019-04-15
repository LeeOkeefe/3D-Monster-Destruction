using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class LampCollision : MonoBehaviour
    {
        private GameObject m_StreetLamp;

        private void Start()
        {
            m_StreetLamp = GetComponentInChildren<LampCollision>().gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (gameObject != null)
                {
                    m_StreetLamp.transform.Rotate(Vector3.forward, 90, Space.Self);
                    m_StreetLamp.transform.Rotate(Vector3.right, 90, Space.Self);
                }

                Destroy(this);
            }
        }
    }
}
