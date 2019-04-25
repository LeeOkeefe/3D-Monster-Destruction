using AI;
using Objects.Destructible.Definition;
using Objects.Destructible.Objects;
using Player;
using UnityEngine;

public class CarBehaviour : MonoBehaviour, IDeathHandler
{
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float scoreAwarded = 10;
    [SerializeField]
    private Color m_Colour;

    private Renderer m_Renderer;

    private void Start()
    {
        m_Colour = new Color(Random.Range(0F, 0.45F), Random.Range(0F, 0.45F), Random.Range(0F, 0.45F));
        m_Renderer = GetComponentInChildren<MeshRenderer>();
        m_Renderer.material.SetColor("_Color", m_Colour);
    }

    private void OnCollisionEnter(Collision other)
    {
        var destructible = other?.gameObject.GetComponent<IDestructible>();
        var player = other?.gameObject.GetComponentInParent<PlayerController>();

        if (destructible == null && player == null)
            return;

        HandleDeath();
    }

    public void HandleDeath()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ScoreManager.AddScore(scoreAwarded, 10);
        Destroy(gameObject);
    }
}
