using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class RunStateMachine : StateMachineBehaviour
    {
        private static readonly int CanRun = Animator.StringToHash("CanRun");

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.Play(animator.GetBool(CanRun) ? "Run" : "Walk", 0);
        }
    }
}
