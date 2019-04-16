using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField]
    private GameObject brokenObject;
    [SerializeField]
    private GameObject[] objectsToDestroy;
    [SerializeField]
    private float destructionPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            brokenObject.SetActive(true);
            gameObject.transform.DetachChildren();

            foreach (var go in objectsToDestroy)
            {
                Destroy(go);
            }

            Destroy(gameObject);
            ScoreManager.AddScore(destructionPoints);
        }
    }
}
