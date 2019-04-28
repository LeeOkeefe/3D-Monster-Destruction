using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace AI.Enemies
{
    internal sealed class EnemySpawn : MonoBehaviour
    {
        [SerializeField]
        private Enemy[] enemies;
        [SerializeField]
        private GameObject[] spawnPositions;
        [SerializeField]
        private float timeTillSpawn;

        private float m_Timer;

        public void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= timeTillSpawn)
            {
                Instantiate(RandomSpawn(enemies), RandomSpawn(spawnPositions).transform.position, Quaternion.identity);
                m_Timer = 0;
            }
        }

        /// <summary>
        /// Returns a random element from the array
        /// </summary>
        private static T RandomSpawn<T>(IReadOnlyList<T> array)
        {
            var rand = new Random();
            var pos = rand.Next(0, array.Count);

            return array[pos];
        }
    }
}
