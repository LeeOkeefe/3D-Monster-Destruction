using UnityEngine;

namespace Objects.Destructible.Definition
{
    internal interface IShatter
    {
        void Explode(float force, Vector3 direction, float radius);
    }
}