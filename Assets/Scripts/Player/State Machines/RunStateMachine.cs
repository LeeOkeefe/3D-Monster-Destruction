using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class RunStateMachine : StateMachineBehaviour
    {
        private PlayerStats m_PlayerStats;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            m_PlayerStats = GameManager.instance.playerStats;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!animator.GetBool("CanRun"))
            {
                animator.Play("Walk", 0);
            }
            else if (animator.GetBool("CanRun"))
            {
                animator.Play("Run", 0);
            }
        }
    }
}
