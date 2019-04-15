using UnityEngine;

namespace Traffic_System
{
    internal sealed class CarAI : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private float scoreAwarded = 30;

        private bool m_IsStopped;
        private float m_TurnTimer = 1;

        public Color m_Colour;

        private Renderer m_Renderer;

        private void Start()
        {
            m_Colour = new Color(Random.Range(0F, 0.45F), Random.Range(0F, 0.45F), Random.Range(0F, 0.45F));
            m_Renderer = GetComponentInChildren<MeshRenderer>();
            m_Renderer.material.SetColor("_Color", m_Colour);
        }

        private void Update()
        {
            if (!m_IsStopped)
            {
                Drive();
            }

            m_TurnTimer += Time.deltaTime;
        }

        public void Drive()
        {
            transform.Translate((Vector3.forward * Time.deltaTime) * speed);
        }

        public void TurnLeft()
        {
            if (!(m_TurnTimer > 0.3f))
                return;

            var rotation = transform.rotation;
            transform.Rotate(rotation.x, -90, rotation.z);
            m_TurnTimer = 0;
        }

        public void TurnRight()
        {
            if (!(m_TurnTimer > 0.3f))
                return;

            var rotation = transform.rotation;
            transform.Rotate(rotation.x, 90, rotation.z);
            m_TurnTimer = 0;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                m_IsStopped = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                m_IsStopped = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = GameManager.instance.player;

            if (other.gameObject.CompareTag("Right Foot") && player.PlayerIsMoving ||
                other.gameObject.CompareTag("Left Foot") && player.PlayerIsMoving ||
                other.gameObject.CompareTag("Tree"))
                
            {
                Destroy(gameObject);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                ScoreManager.AddScore(scoreAwarded, 10);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Building"))
                return;

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            ScoreManager.AddScore(scoreAwarded, 30);
            Destroy(gameObject);
        }
    }
}
