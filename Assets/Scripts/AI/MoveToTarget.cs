using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    internal sealed class MoveToTarget : MonoBehaviour
    {
        private NavMeshAgent m_NavMeshAgent;
        private Transform PlayerTransform => GameManager.instance.player.transform;

        private void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            InvokeRepeating("SetTarget", 2F, RandomNumberGenerator(2, 10));
        }

        /// <summary>
        /// Sets the destination of the Nav Actor
        /// </summary>
        private void SetTarget()
        {
            if (m_NavMeshAgent == null)
            {
                Debug.Log($"Nav Mesh Agent is null on {gameObject.name}");
            }

            if (PlayerTransform == null)
            {
                Debug.Log($"{PlayerTransform} was null");
            }

            if (!m_NavMeshAgent.isOnNavMesh)
                return;

            m_NavMeshAgent.SetDestination(PlayerTransform.position);
        }

        /// <summary>
        /// Generates a random number between a min and max value
        /// </summary>
        private static float RandomNumberGenerator(float min, float max)
        {
            var randomNumber = Random.Range(min, max);

            return randomNumber;
        }
    }
}
