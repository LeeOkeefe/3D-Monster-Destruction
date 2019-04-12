using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class AttackStateMachine : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int index)
        {
            anim.SetBool("IsAttacking", true);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int index)
        {
            anim.SetBool("IsAttacking", false);
        }
    }
}
