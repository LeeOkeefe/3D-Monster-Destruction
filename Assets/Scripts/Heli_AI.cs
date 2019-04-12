using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heli_AI : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject rotor1;
    [SerializeField]
    GameObject rotor2;
    [SerializeField]
    GameObject lGun;
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float stopDistance;
    [SerializeField]
    float followDistance;
    [SerializeField]
    float shootDistance;

    float shootTimer;
    bool canShoot;

    [SerializeField]
    float distanceToPlayer;

    // Use this for initialization
    void Start()
    {
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        transform.position = new Vector3(transform.position.x, 15, transform.position.z);

        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > followDistance)
        {
            transform.Translate(0, 0, 0.1f);
        }
        else if (distanceToPlayer < stopDistance)
        {
            transform.Translate(0, 0, -0.1f);
        }


        if (distanceToPlayer < shootDistance)
        {
            if (canShoot)
            {
                Instantiate(bullet, lGun.transform.position, transform.rotation);
                canShoot = false;
                shootTimer = 0;
            }

        }

        rotor1.transform.Rotate(0, 10, 0);
        rotor2.transform.Rotate(0, 10, 0);

        shootTimer += Time.deltaTime;
        if (shootTimer >= 1)
        {
            canShoot = true;
        }


    }


}
