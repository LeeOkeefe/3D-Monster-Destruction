﻿using Objects.Interactable;
using UI.Settings.Audio;
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
        [SerializeField]
        private AudioClip firingClip;
        [SerializeField]
        private AudioClip explosion;
        [SerializeField]
        private AudioClip damagedClip;

        protected AudioSource AudioSource => GetComponent<AudioSource>();

        protected static GameObject FireToPoint => GameManager.Instance.playerShootingPosition;
        protected bool IsHoldingObject => GetComponent<ObjectInteraction>().HoldingObject;
        public Quaternion OriginalRotation { get; protected set; }

        protected RaycastHit hit;
        protected bool CanShootPlayer => TimeTillShoot <= 0;
        protected float TimeTillShoot;

        /// <summary>
        /// Looks toward the player, if raycast hits the player and we can shoot,
        /// then we shoot the player
        /// </summary>
        public override void Attack()
        {
            firingPoint.transform.LookAt(FireToPoint.transform.position);

            if (!Physics.Raycast(firingPoint.transform.position, Vector3.forward, out hit))
                return;

            if (!hit.transform.CompareTag("Player") && !(TimeTillShoot <= 0))
                return;

            SoundEffectManager.Instance.PlayClipAtPoint(firingClip, transform.position);
            var position = firingPoint.transform.position;
            Instantiate(firingEffect, position, Quaternion.identity);
            Instantiate(projectile, position, firingPoint.transform.rotation);
            TimeTillShoot = timeBetweenAttacks;
        }

        // Handles the tank being damaged/killed
        //
        public override void HandleDeath()
        {
            Damage(environmentalDamage);

            if (EnemyIsDead)
            {
                SoundEffectManager.Instance.PlayClipAtPoint(explosion, transform.position);
                Instantiate(tankExplodingPrefab, transform.position, Quaternion.identity);
                ScoreManager.AddScore(scoreAwarded);
                Destroy(gameObject);
            }
            else
            {
                SoundEffectManager.Instance.PlayClipAtPoint(damagedClip, transform.position);
                Instantiate(tankDamagePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
