using UnityEngine;
using Random = System.Random;

namespace AI
{
    internal sealed class EnemySpawn : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;
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
                Instantiate(enemy, RandomSpawnLoc().transform.position, Quaternion.identity);
                m_Timer = 0;
            }
        }

        /// <summary>
        /// Get random GameObject from the array of position
        /// </summary>
        private GameObject RandomSpawnLoc()
        {
            var rand = new Random();
            var pos = rand.Next(0, spawnPositions.Length);

            return spawnPositions[pos];
        }
    }
}
