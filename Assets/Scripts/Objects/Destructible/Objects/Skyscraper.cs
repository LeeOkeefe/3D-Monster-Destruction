using System.Linq;
using AI;
using Extensions;
using Objects.Destructible.Definition;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects.Destructible.Objects
{
    internal sealed class Skyscraper : DestructibleObject
    {
        [SerializeField]
        private GameObject[] fragments;
        [SerializeField]
        private GameObject explosion, flames;
        [SerializeField]
        private GameObject regularRubble, burntRubble;

        private Vector3 m_Pos;

        private MeshRenderer[] m_Renderer;
        private Color m_Colour, m_EmissionColour;
        private float m_Intensity;

        private const float BelowGroundLevel = -8;

        private bool m_Collapsed;
        private bool m_Regular;

        private void Start()
        {
            m_Pos = transform.position;
            GenerateRandomColour();
        }

        private void Update()
        {
            if (m_Collapsed && m_Regular)
            {
                Collapse(regularRubble);
            }
            else
            {
                Collapse(burntRubble);
            }
        }

        /// <summary>
        /// Generate a random colours and light intensity for the building
        /// </summary>
        private void GenerateRandomColour()
        {
            m_Colour = new Color(Random.Range(0.65F, 1F), Random.Range(0.65F, 1F), Random.Range(0.65F, 1F));
            m_EmissionColour = new Color(Random.Range(0.6F, 1F), Random.Range(0.6F, 1F), Random.Range(0.6F, 1F));
            m_Intensity = Random.Range(0.3f, 0.8f);

            m_Renderer = GetComponentsInChildren<MeshRenderer>();

            foreach (var meshRenderer in m_Renderer)
            {
                meshRenderer.material.SetColor("_Color", m_Colour);
                meshRenderer.material.SetColor("_EmissionColor", m_EmissionColour * m_Intensity);
            }
        }

        /// <summary>
        /// Checks percentage of building health and enables fragments
        /// depending on the percentage
        /// </summary>
        private void CheckBuildingHealth()
        {
            var healthPercentage = maxHealth / 100;

            if (fragments.Any() && currentHealth <= healthPercentage * 75)
            {
                EnableFragments(fragments[0]);
            }

            if (fragments.Any() && currentHealth <= healthPercentage * 40)
            {
                EnableFragments(fragments[1]);
            }
        }

        /// <summary>
        /// Adds a rigidBody to the fragments, then destroys after 5 seconds
        /// </summary>
        private static void EnableFragments(GameObject fragment)
        {
            foreach (Transform child in fragment.transform)
            {
                var frag = child.gameObject;

                if (frag.GetComponent<Rigidbody>() != null)
                    continue;

                frag.AddComponent<Rigidbody>();
                Destroy(frag, 5f);
            }
        }

        // Remove parent from the children if object has been destroyed
        //
        public override void Destruct()
        {
            if (!IsObjectDestroyed)
                return;

            Instantiate(explosion, m_Pos, Quaternion.identity);

            foreach (var fragment in fragments)
            {
                fragment.transform.parent = null;
            }
        }

        /// <summary>
        /// Collapse the building and destroy it once it goes beneath the ground level
        /// </summary>
        private void Collapse(GameObject rubble)
        {
            if (!IsObjectDestroyed)
                return;

            StartCoroutine(Camera.main.Shake(2, 1));
            transform.Rotate(Random.insideUnitSphere * 0.5f);
            transform.Translate(Vector3.down * 3 * Time.deltaTime);

            if (transform.position.y < BelowGroundLevel)
            {
                Instantiate(rubble, m_Pos, Quaternion.identity);
            }

            if (transform.position.y < -13)
            {
                Destroy(gameObject);
            }
        }

        // Add score per hit, based on the health of the building
        //
        public override void Damage(float damage)
        {
            var scorePerHit = scoreAwarded / maxHealth;
            currentHealth -= damage;

            ScoreManager.AddScore(damage * scorePerHit, 10);
        }

        /// <summary>
        /// Checks the total damage and applies it to the building
        /// </summary>
        public void FlamethrowerDamage()
        {
            if (GameManager.instance.playerStats.TotalDamage <= 10)
                return;

            Damage(GameManager.instance.playerStats.TotalDamage);
            flames.SetActive(true);

            if (currentHealth <= 0)
            {
                m_Regular = false;
                m_Collapsed = true;
            }
        }

        // Check that our player's hands have collided with us (the building)
        //
        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger && other.gameObject.CompareTag("Left Hand") ||
                other.isTrigger && other.gameObject.CompareTag("Right Hand"))
            {
                var playerStats = other.GetComponentInParent<PlayerStats>();
                Damage(playerStats.TotalDamage);

                CheckBuildingHealth();
            }

            if (other.gameObject.CompareTag("Car"))
            {
                Damage(40);
            }

            var handler = other.gameObject.GetComponent<IDeathHandler>();
            handler?.HandleDeath();

            CheckBuildingHealth();
            m_Collapsed = true;
            m_Regular = true;

            if (IsObjectDestroyed)
            {
                Destruct();
            }
        }
    }
}
