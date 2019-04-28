using Objects.Destructible.Definition;
using Objects.Destructible.Objects;
using Objects.Interactable;
using UnityEngine;

namespace AI.Enemies
{
    internal sealed class Helicopter : Enemy
    {
        [SerializeField]
        private GameObject fireEffect;
        [SerializeField]
        private GameObject helicopterExplosion;
        [SerializeField]
        private GameObject projectile;

        [SerializeField]
        private GameObject topRotor;
        [SerializeField]
        private GameObject backRotor;
        [SerializeField]
        private GameObject gun;

        private bool CanShoot => m_TimeTillShoot <= 0;

        private float m_TimeTillShoot;

        private RaycastHit m_Hit;

        private bool IsHoldingObject => GetComponent<ObjectInteraction>().HoldingObject;
        private GameObject Target => GameManager.instance.playerShootingPosition;

        private void Update()
        {
            if (!IsHoldingObject)
            {
                transform.LookAt(Target.transform);

                HandleRotors();

                if (!CanShoot)
                {
                    m_TimeTillShoot -= Time.deltaTime;
                }

                if (IsPlayerInRange(distanceToAttackTarget) && CanShoot)
                {
                    Attack();
                }
            }
        }

        // Check to see if the raycast hit the player & whether we can fire again
        // Instantiate a projectile and reset the timer
        //
        public override void Attack()
        {
            if (IsHoldingObject)
                return;

            if (!Physics.Raycast(gun.transform.position, transform.forward, out m_Hit))
                return;

            if (!m_Hit.transform.CompareTag("Player") && !(m_TimeTillShoot <= 0))
                return;

            Instantiate(projectile, gun.transform.position, transform.rotation);
            m_TimeTillShoot = timeBetweenAttacks;
        }

        /// <summary>
        /// Stops the rotors rotating when we are picked up
        /// </summary>
        private void HandleRotors()
        {
            if (!IsHoldingObject)
            {
                topRotor.transform.Rotate(0, 10, 0);
                backRotor.transform.Rotate(0, 10, 0);
            }
            else
            {
                topRotor.transform.Rotate(0, 0, 0);
                backRotor.transform.Rotate(0, 0, 0);
            }
        }

        // Handle damage/collision
        //
        public override void HandleDeath()
        {
            Damage(environmentalDamage);

            if (!EnemyIsDead)
                return;

            Instantiate(helicopterExplosion, transform.position, Quaternion.identity);
            ScoreManager.AddScore(scoreAwarded);
            Destroy(gameObject);
        }

        // Check that our player's hands have collided with us and handle damage
        // Check if we collide with a destructibleObject and handle damage
        //
        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger && other.gameObject.CompareTag("Left Hand") ||
                other.isTrigger && other.gameObject.CompareTag("Right Hand"))
            {
                HandleDeath();
            }

            if (other.gameObject.GetComponentInChildren<DestructibleObject>())
            {
                var dest = other.gameObject.GetComponent<IDestructible>();
                dest.Damage(environmentalDamage);
                dest.Destruct();

                HandleDeath();
            }
        }

        // Deal damage and instantiate particle effects
        // for burning helicopter
        //
        public void BurnHelicopter()
        {
            var position = transform.position;

            Instantiate(fireEffect, position, Quaternion.identity);

            currentHealth -= currentHealth;

            ScoreManager.AddScore(scoreAwarded);
            Destroy(gameObject);
            Instantiate(helicopterExplosion, position, Quaternion.identity);
        }
    }
}
