using AI;
using Objects.Destructible.Definition;
using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class JumpStateMachine : StateMachineBehaviour
    {
        [SerializeField]
        private GameObject stompEffect;

        private GameObject m_Shockwave;

        private static readonly int Shockwave = Animator.StringToHash("Shockwave");

        // Check that the animation is half way through playing, then activate shockwave/explosion damage
        // Parent the shockwave to the parent in case the player is walking/running and activates it
        // Use boolean to ensure we only activate it once per jump
        //
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5F && !animator.GetBool(Shockwave))
            {
                animator.SetBool(Shockwave, true);
                m_Shockwave = Instantiate(stompEffect, animator.rootPosition, animator.rootRotation);
                GameManager.Instance.CameraShake();
                m_Shockwave.transform.parent = GameManager.Instance.player.transform;
                ExplosionDamage(animator.transform.position, 6F);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        // Set boolean to false at the end of the jump, so we can activate the shockwave again 
        //
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Shockwave, false);
        }

        /// <summary>
        /// Sends out overlap sphere and collects all hit colliders in a collection
        /// Then call the HandleDeath method if any of them contain it
        /// </summary>
        private static void ExplosionDamage(Vector3 center, float radius)
        {
            var hitColliders = Physics.OverlapSphere(center, radius);

            foreach (var hitCollider in hitColliders)
            {
                hitCollider.GetComponent<IDeathHandler>()?.HandleDeath();
                hitCollider.GetComponent<IShatter>()?.Explode(50, Vector3.one, 50);
            }
        }
    }
}
