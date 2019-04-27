using System.Linq;
using UnityEngine;

namespace Extensions
{
    internal static class GameObjectExtensions
    {
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
        }
    }
}
