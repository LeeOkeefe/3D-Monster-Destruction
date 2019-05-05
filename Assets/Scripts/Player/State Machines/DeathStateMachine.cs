using UnityEngine;

namespace Player.State_Machines
{
    internal sealed class DeathStateMachine : StateMachineBehaviour
    {
        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        /*public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=
                animator.GetCurrentAnimatorStateInfo(0).length)
                return;

            GameManager.Instance.GameOver();
        }*/
    }
}
