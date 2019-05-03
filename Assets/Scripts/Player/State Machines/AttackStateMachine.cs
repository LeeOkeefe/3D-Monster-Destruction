using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class AttackStateMachine : StateMachineBehaviour
    {
        private static readonly int AttackTrail = Animator.StringToHash("AttackTrail");
        private static Collider PlayerRightHand => GameManager.instance.playerRightHand;
        private static Collider PlayerLeftHand => GameManager.instance.playerLeftHand;
        private TrailRenderer RightTrail => PlayerRightHand.GetComponent<TrailRenderer>();
        private TrailRenderer LeftTrail => PlayerLeftHand.GetComponent<TrailRenderer>();

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //
        public override void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int index)
        {
            PlayerRightHand.isTrigger = true;
            PlayerLeftHand.isTrigger = true;
            ModifyTrailColours();
        }

        // Enable trail renderers once 25% of the animation has played
        //
        public override void OnStateUpdate(Animator anim, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.25F && !anim.GetBool(AttackTrail))
            {
                anim.SetBool(AttackTrail, true);
                RightTrail.enabled = true;
                LeftTrail.enabled = true;
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //
        public override void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int index)
        {
            RightTrail.enabled = false;
            LeftTrail.enabled = false;
            PlayerRightHand.isTrigger = false;
            PlayerLeftHand.isTrigger = false;
            anim.SetBool(AttackTrail, false);
        }

        /// <summary>
        /// Modify trail colours depending on whether the strength is boosted
        /// </summary>
        private void ModifyTrailColours()
        {
            if (GameManager.instance.playerStats.BoostedDamage > 0)
            {
                RightTrail.material.color = Color.red;
                LeftTrail.material.color = Color.red;
            }
            else
            {
                RightTrail.material.color = Color.white;
                LeftTrail.material.color = Color.white;
            }
        }
    }
}
