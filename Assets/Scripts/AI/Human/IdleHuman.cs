using UnityEngine;

namespace AI.Human
{
    internal sealed class IdleHuman : MonoBehaviour, IDeathHandler
    {
        [SerializeField]
        private GameObject bloodSplatter;
        [SerializeField]
        private float scoreAwarded = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            HandleDeath();
        }

        // Handle death for human, instantiate particles, add score and destroy gameObject
        //
        public void HandleDeath()
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.Euler(0, 0, 90));
            ScoreManager.AddScore(scoreAwarded);
            Destroy(gameObject);
        }
    }
}
