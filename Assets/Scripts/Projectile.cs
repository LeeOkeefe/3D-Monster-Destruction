using Objects.Destructible.Definition;
using Objects.Destructible.Objects;
using Player;
using UnityEngine;

internal sealed class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float projectileDamage;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private GameObject explosion;

    private Camera m_MainCamera;
    private Rigidbody m_Rb;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        SetVelocity(projectileSpeed);

        // Destroy ourselves if we have gone off the screen
        if (!IsOnScreen())
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sends the projectile using direction and speed
    /// </summary>
    public void SetVelocity(float speed)
    {
        m_Rb.velocity = transform.forward * speed;
    }

    // Check what has collided and handle the collision
    //
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tank") || other.gameObject.CompareTag("Projectile"))
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            var playerHealth = other.GetComponent<PlayerStats>();
            playerHealth.Damage(projectileDamage);
        }

        if (other.gameObject.GetComponent(typeof(DestructibleObject)))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            var destructibleObject = GetComponent(typeof(DestructibleObject)) as IDestructible;
            destructibleObject?.Damage(20);
            destructibleObject?.Destruct();
        }

        Destroy(gameObject);
    }

    private bool IsOnScreen()
    {
        var position = transform.position;

        // Get coordinates on the screen between 0 and 1
        // ensures this method will work consistently on all resolutions
        var screenCoordinates = m_MainCamera.WorldToViewportPoint(position);

        // Check the returned coordinates to see if we're on screen
        return screenCoordinates.x < 1
            && screenCoordinates.x > 0
            && screenCoordinates.y < 1
            && screenCoordinates.y > 0;
    }
}
