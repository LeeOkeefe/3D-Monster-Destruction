using System.Linq;
using UnityEngine;

namespace Extensions
{
    internal static class GameObjectExtensions
    {
        /// <summary>
        /// Checks whether the gameObject or it's children contain the specified component
        /// </summary>
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
        }
    }
}
