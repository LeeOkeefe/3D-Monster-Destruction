using UnityEngine;

namespace Objects.Interactable
{
    public class BreakObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject brokenObject;
        [SerializeField]
        private GameObject[] objectsToDestroy;
        [SerializeField]
        private float destructionPoints;

        // Set second gameObject active, detach children and destroy the parent
        //
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
}
