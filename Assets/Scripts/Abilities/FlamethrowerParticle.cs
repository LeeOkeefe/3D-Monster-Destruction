using UnityEngine;
using AI.Enemies;
using Extensions;
using Objects.Destructible.Objects;
using Objects.Destructible.Objects.ForceObjects;
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
                if (!other.gameObject.HasComponent<Skyscraper>())
                    return;

                var building = other.GetComponent<Skyscraper>();
                building.FlamethrowerDamage();
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
