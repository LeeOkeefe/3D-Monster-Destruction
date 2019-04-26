using Objects.Destructible.Objects;
using Player;
using UnityEngine;

namespace AI
{
    internal sealed class CarBehaviour : MonoBehaviour, IDeathHandler
    {
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private float scoreAwarded = 10;
        [SerializeField]
        private Color m_Colour;
        [SerializeField]
        private bool autoColourVehicle = true;

        private Renderer m_Renderer;

        // Randomly generate colour for vehicle unless bool is set to false via inspector
        //
        private void Start()
        {
            if (!autoColourVehicle)
                return;

            m_Colour = new Color(Random.Range(0F, 0.45F), Random.Range(0F, 0.45F), Random.Range(0F, 0.45F));
            m_Renderer = GetComponentInChildren<MeshRenderer>();
            m_Renderer.material.SetColor("_Color", m_Colour);
        }

        // Check that the other collider implements IDestructible or is the Player
        // HandleDeath if true, return if false
        //
        private void OnTriggerEnter(Collider other)
        {
            var destructible = other.gameObject.GetComponent<IDestructible>();
            var player = other.gameObject.GetComponentInParent<PlayerController>();

            if (destructible == null && player == null)
                return;

            HandleDeath();
        }

        // Handles death of vehicle
        // 
        public void HandleDeath()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            ScoreManager.AddScore(scoreAwarded, 10);
            Destroy(gameObject);
        }
    }
}
