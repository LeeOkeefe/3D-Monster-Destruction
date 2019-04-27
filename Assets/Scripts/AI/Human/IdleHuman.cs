using UnityEngine;

namespace AI.Human
{
    internal sealed class IdleHuman : MonoBehaviour, IDeathHandler
    {
        [SerializeField]
        private GameObject bloodSplatter;
        [SerializeField]
        private float scoreAwarded = 5;

        private Animator m_Animator;

        private static readonly int Idle = Animator.StringToHash("Idle");

        // Get random number to select randomly which of the two idle 
        // animations to play
        //
        private void Start ()
        {
            m_Animator = GetComponent<Animator>();

            var num = Random.Range(0, 3);

            m_Animator.SetInteger(Idle, num);
        }

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
