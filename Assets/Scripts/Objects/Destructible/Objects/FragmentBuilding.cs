using System.Linq;
using AI;
using Objects.Destructible.Definition;
using Player;
using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class FragmentBuilding : DestructibleObject
    {
        [SerializeField]
        private GameObject[] fragments;
        [SerializeField]
        private GameObject explosion;
        [SerializeField]
        private GameObject rubble;

        public GameObject burntRubble;

        private Building m_Building;
        public void HandleFlamethrower() => m_Building.FlamethrowerDamage(this, DestroyObject);
        private void InstantiateExplosion() => Instantiate(explosion, m_Pos, Quaternion.identity);

        private Vector3 m_Pos;
        private bool m_SpawnedRubble;
        private bool m_HasHappenedOnce;

        private MeshRenderer[] m_Renderer;
        private Color m_Colour;
        private Color m_EmissionColour;
        private float m_Intensity;

        private const float BelowGroundLevel = -8;

        // Instantiate new building, grab and set random colour (within range)
        // for the building colour and lights (emission) which includes intensity
        //
        private void Start()
        {
            m_Building = new Building();
            m_Colour = new Color(Random.Range(0.65F, 1F), Random.Range(0.65F, 1F), Random.Range(0.65F, 1F));
            m_EmissionColour = new Color(Random.Range(0.6F, 1F), Random.Range(0.6F, 1F), Random.Range(0.6F, 1F));
            m_Intensity = Random.Range(0.3f, 0.8f);

            m_Renderer = GetComponentsInChildren<MeshRenderer>();
            m_Pos = transform.position;

            foreach (var meshRenderer in m_Renderer)
            {
                meshRenderer.material.SetColor("_Color", m_Colour);
                meshRenderer.material.SetColor("_EmissionColor", m_EmissionColour * m_Intensity);
            }
        }

        // If object isn't destroyed, don't execute the rest of the code.
        // Handle destruction, and explode/add score, ensuring we only do it once
        //
        private void Update()
        {
            if (!IsObjectDestroyed)
                return;

            HandleBuildingDestroyed();

            if (!m_HasHappenedOnce)
            {
                InstantiateExplosion();
                ScoreManager.AddScore(scoreAwarded);
                m_HasHappenedOnce = true;
            }
        }

        /// <summary>
        /// Checks the health, and activates fragments depending on the percentage
        /// </summary>
        public override void Destruct()
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

            HandleBuildingDestroyed();
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

        /// <summary>
        /// Checks the building state, and destroys it if the health is 0,
        /// if it is, instantiate explosion prefab
        /// </summary>
        private void HandleBuildingDestroyed()
        {
            if (!IsObjectDestroyed)
                return;

            foreach (var fragment in fragments)
            {
                fragment.transform.parent = null;
            }

            transform.Rotate(Random.insideUnitSphere * 0.5f);
            transform.Translate(Vector3.down * 3 * Time.deltaTime);

            if (transform.position.y < BelowGroundLevel && !m_SpawnedRubble)
            {
                Instantiate(rubble, m_Pos, Quaternion.identity);
                m_SpawnedRubble = true;
            }

            if (transform.position.y < -13)
            {
                Destroy(gameObject);
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
                m_Building.Damage(this, playerStats.TotalDamage);
                Destruct();
            }
        }

        // Checks to see if there is a collision with a car and handles damage
        //
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Tank"))
            {
                m_Building.Damage(this, 25);
                Destruct();
            }

            var enemy = other.gameObject.GetComponent(typeof(Enemy)) as IDeathHandler;
            enemy?.HandleDeath();
        }
    }
}
