using UnityEngine;

internal sealed class ThrowStateMachine : StateMachineBehaviour
{
    private static readonly int AttackTrail = Animator.StringToHash("AttackTrail");
    private static Collider PlayerLeftHand => GameManager.instance.playerLeftHand;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool(AttackTrail))
        {
            animator.SetBool(AttackTrail, true);
            PlayerLeftHand.GetComponent<TrailRenderer>().enabled = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerLeftHand.GetComponent<TrailRenderer>().enabled = false;
        animator.SetBool(AttackTrail, false);
    }
}
