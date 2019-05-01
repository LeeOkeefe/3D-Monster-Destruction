using AI;
using UnityEngine;

namespace Player.State_Machines
{
    public class JumpStateMachine : StateMachineBehaviour
    {
        [SerializeField]
        private GameObject stompEffect;

        /// <summary>
        /// Sends out overlap sphere and collects all hit colliders in a collection
        /// Then call the HandleDeath method if any of them contain it
        /// </summary>
        private void ExplosionDamage(Vector3 center, float radius)
        {
            var hitColliders = Physics.OverlapSphere(center, radius);

            foreach (var hitCollider in hitColliders)
            {
                hitCollider.GetComponent<IDeathHandler>()?.HandleDeath();
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameManager.instance.player.ExplosionDamage(animator.transform.position, 7.5F);
        }
    }
}
