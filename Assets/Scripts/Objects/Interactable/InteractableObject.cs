using UnityEngine;

namespace Objects.Interactable
{
    // These fields that are all shared across the objects that can be interacted with
    internal abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField]
        protected float distance = 1f;

        protected Transform Player => GameManager.Instance.player.transform;

        /// <summary>
        /// Calculate the distance between the object and player position
        /// Check if the distance between them is less than the "distance" value
        /// </summary>
        protected bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, Player.position) < distance;
        }
    }
}