using AI.Enemies;
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
        // If we collide with another enemy or projectile, do nothing
        // (We may want to change this in future)
        //
        if (other.GetComponentInParent(typeof(Enemy)) || other.gameObject.CompareTag("Projectile"))
            return;

        // If we collide with the player, damage the player health
        //
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            var playerHealth = other.GetComponent<PlayerStats>();
            playerHealth.Damage(projectileDamage);
        }

        // Checks if we hit a destructibleObject, if we have then
        // call the methods on the IDestructible 
        //
        if (other.gameObject.GetComponent(typeof(DestructibleObject)))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            var destructible = other.GetComponent<IDestructible>();
            destructible?.Damage(20);
            destructible?.Destruct();
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
