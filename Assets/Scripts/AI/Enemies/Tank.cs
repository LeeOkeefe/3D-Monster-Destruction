using Objects.Interactable;
using UnityEngine;

namespace AI.Enemies
{
    internal abstract class Tank : Enemy
    {
        [SerializeField]
        protected GameObject firingPoint;
        [SerializeField]
        protected GameObject firingEffect;
        [SerializeField]
        protected GameObject tankExplodingPrefab;
        [SerializeField]
        protected GameObject tankDamagePrefab;
        [SerializeField]
        protected GameObject turret;
        [SerializeField]
        private GameObject projectile;

        protected static GameObject FireToPoint => GameManager.instance.playerShootingPosition;
        protected bool IsHoldingObject => GetComponent<ObjectInteraction>().HoldingObject;
        public Quaternion OriginalRotation { get; protected set; }

        protected RaycastHit hit;
        protected bool CanShootPlayer => timeTillShoot <= 0;
        protected float timeTillShoot;

        /// <summary>
        /// Looks toward the player, if raycast hits the player and we can shoot,
        /// then we shoot the player
        /// </summary>
        public override void Attack()
        {
            firingPoint.transform.LookAt(FireToPoint.transform.position);

            if (!Physics.Raycast(firingPoint.transform.position, Vector3.forward, out hit))
                return;

            if (!hit.transform.CompareTag("Player") && !(timeTillShoot <= 0))
                return;

            var position = firingPoint.transform.position;
            Instantiate(firingEffect, position, Quaternion.identity);
            Instantiate(projectile, position, firingPoint.transform.rotation);
            timeTillShoot = timeBetweenAttacks;
        }

        // Handles the tank being damaged/killed
        //
        public override void HandleDeath()
        {
            Damage(environmentalDamage);

            if (EnemyIsDead)
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
