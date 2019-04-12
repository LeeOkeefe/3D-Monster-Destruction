using System.Linq;
using TESTING;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawn : MonoBehaviour
{
    public Transform[] possibleSpawnPositions;

    public GameObject objectTypeToSpawn;

    private CarAI[] m_Cars;

    private int m_LastSpawnIndex;

    private void Update()
    {
        m_Cars = FindObjectsOfType<CarAI>();

        if (m_Cars.Count() != 25)
        {
            SpawnNewObject();
        }
    }

    /// <summary>
    /// Instantiates object at random location, ensuring we don't use the previous location used
    /// </summary>
    public void SpawnNewObject()
    {
        var spawnPointReference = GetSpawnPointReference();

        while (spawnPointReference == possibleSpawnPositions[m_LastSpawnIndex].transform)
        {
            spawnPointReference = GetSpawnPointReference();
            break;
        }

        Instantiate(objectTypeToSpawn, spawnPointReference.position, spawnPointReference.rotation);
    }

    /// <summary>
    /// Return spawn location using random index
    /// </summary>
    public Transform GetSpawnPointReference()
    {
        var randomIndex = Random.Range(0, possibleSpawnPositions.Length);
        m_LastSpawnIndex = randomIndex;
        return possibleSpawnPositions[randomIndex];
    }
}
