using System.Security.Cryptography;
using Traffic_System;
using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class FireHydrant : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particleEffect;

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
                Instantiate(particleEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            }
        }
    }
}
