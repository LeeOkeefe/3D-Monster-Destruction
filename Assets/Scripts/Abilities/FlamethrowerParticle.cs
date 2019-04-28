using UnityEngine;
using AI;
using AI.Enemies;
using Objects.Destructible.Objects;
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

                if (building.currentHealth <= 0)
                    Instantiate(building.burntRubble, other.gameObject.transform.position, Quaternion.identity);
            }

            if (other.gameObject.CompareTag("Tree"))
            {
                var tree = other.GetComponent<Tree>();
                tree.BurnTree();
            }

            if (other.gameObject.CompareTag("Helicopter"))
            {
                var enemy = other.GetComponent<Helicopter>();
                enemy.BurnHelicopter();
            }
        }
    }
}
