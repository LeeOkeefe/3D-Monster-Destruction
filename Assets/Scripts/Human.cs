using UnityEngine;

internal sealed class Human : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodSplatter;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        Instantiate(bloodSplatter, transform.position, Quaternion.Euler(0,0,90));
        ScoreManager.AddScore(10);
        Destroy(gameObject);
    }
}
