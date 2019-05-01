using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class AttackStateMachine : StateMachineBehaviour
    {
        private static Collider PlayerRightHand => GameManager.instance.playerRightHand;
        private static Collider PlayerLeftHand => GameManager.instance.playerLeftHand;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int index)
        {
            PlayerRightHand.GetComponent<TrailRenderer>().enabled = true;
            PlayerLeftHand.GetComponent<TrailRenderer>().enabled = true;
            PlayerRightHand.isTrigger = true;
            PlayerLeftHand.isTrigger = true;
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //
        //}
        public override void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int index)
        {
            PlayerRightHand.GetComponent<TrailRenderer>().enabled = false;
            PlayerLeftHand.GetComponent<TrailRenderer>().enabled = false;
            PlayerRightHand.isTrigger = false;
            PlayerLeftHand.isTrigger = false;
        }
    }
}
