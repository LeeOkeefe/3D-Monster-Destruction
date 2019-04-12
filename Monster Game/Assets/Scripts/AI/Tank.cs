using Objects.Destructible.Definition;
using UnityEngine;

namespace AI
{
    internal sealed class Tank : Enemy
    {
        [SerializeField]
        private GameObject firingPoint;
        [SerializeField]
        private GameObject firingEffect;
        [SerializeField]
        private GameObject tankExplodingPrefab;
        [SerializeField]
        private GameObject tankDamagePrefab;

        private RaycastHit m_Hit;
        private bool CanShootPlayer => m_TimeTillShoot <= 0;
        private float m_TimeTillShoot;

        private void Start()
        {
            InitializeHealth();
        }

        void Update()
        {
            if (!CanShootPlayer)
            {
                m_TimeTillShoot -= Time.deltaTime;
            }

            if (IsPlayerInRange(distanceToAttackTarget) && CanShootPlayer)
            {
                ShootPlayer();
            }
        }

        /// <summary>
        /// Looks toward the player, if raycast hits the player and we can shoot,
        /// then we shoot the player
        /// </summary>
        private void ShootPlayer()
        {
            firingPoint.transform.LookAt(PlayerTransform);

            if (Physics.Raycast(firingPoint.transform.position, Vector3.forward, out m_Hit))
            {
                if (!m_Hit.transform.CompareTag("Player") && !(m_TimeTillShoot <= 0))
                    return;

                Instantiate(firingEffect, firingPoint.transform.position, Quaternion.identity);
                PlayerStats.Damage(dealDamage);
                m_TimeTillShoot = timeBetweenAttacks;
            }
        }

        // Deals damage to the building it collides with, and itself
        // Checks whether it is dead and adds score/destroys itself
        //
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Building"))
                return;

            var destructibleObject = other.gameObject.GetComponent<DestructibleObject>();
            destructibleObject.currentHealth -= dealDamage * 6f;

            currentHealth -= dealDamage * 7.5f;

            if (currentHealth <= 0)
            {
                Instantiate(tankExplodingPrefab, transform.position, Quaternion.identity);
                ScoreManager.AddScore(scoreAwarded);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(tankDamagePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
