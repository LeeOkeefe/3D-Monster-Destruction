using Objects.Destructible.Definition;
using UnityEngine;

namespace AI.Enemies
{
    internal sealed class NewTank : Tank
    {
        private void Start()
        {
            InitializeHealth();
        }

        void Update()
        {
            if (!IsHoldingObject)
            {
                turret.transform.LookAt(PlayerTransform);

                if (!CanShootPlayer)
                {
                    TimeTillShoot -= Time.deltaTime;
                }

                if (IsPlayerInRange(distanceToAttackTarget) && CanShootPlayer)
                {
                    Attack();
                }
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
            destructibleObject.currentHealth -= environmentalDamage;

            HandleDeath();
        }
    }
}