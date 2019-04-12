using Objects.Destructible.Objects;
using UnityEngine;
using Tree = Objects.Destructible.Objects.Tree;

namespace Abilities
{
    internal sealed class FlamethrowerParticle : MonoBehaviour
    {
        // Triggers an event when the particle collides with a GameObject
        //
        private void OnParticleCollision(GameObject other)
        {
            if (other.gameObject.CompareTag("Building"))
            {
                var building = other.GetComponent<FragmentBuilding>();
                building.HandleFlamethrower();
            }

            if (other.gameObject.CompareTag("Tree"))
            {
                var tree = other.GetComponent<Tree>();
                tree.BurnTree();
            }
        }
    }
}
