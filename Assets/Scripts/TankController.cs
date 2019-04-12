using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    GameObject hull;
    [SerializeField]
    GameObject turret;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject firingPoint;
    [SerializeField]
    GameObject forwardPosMarker;
    [SerializeField]
    GameObject bullet;


    bool targetPlayer = false;
    bool canShoot = true;
    float shootTimer = 0;

    float speed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        Looker();
        if (targetPlayer)
        {
            Vector3 targetDir = player.transform.position - turret.transform.position;
            float fraction = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(turret.transform.forward, targetDir, fraction, 0.0f);
            turret.transform.rotation = Quaternion.LookRotation(newDir);

        }
        else
        {
            Vector3 targetDir = forwardPosMarker.transform.position;

            // The step size is equal to speed times frame time.
            float fraction = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(turret.transform.forward, targetDir, fraction, 0.0f);

            // Move our position a step closer to the target.
            turret.transform.rotation = Quaternion.LookRotation(newDir);



        }

    }

    void Looker()
    {
        Vector3 targetDir = player.transform.position - turret.transform.position;
        float angle = Vector3.Angle(targetDir, turret.transform.forward);
        float distance = Vector3.Distance(transform.position, player.transform.position);

        Debug.Log(distance);

        if (distance > 30)
        {
            targetPlayer = false;
        }
        else
        {
            targetPlayer = true;
        }
        
        if (angle < 2.0f)
        {
            if (shootTimer > 3)
            {
                Instantiate(bullet, firingPoint.transform.position, firingPoint.transform.rotation);
                shootTimer = 0;

                //Need to add a raycast to player to make sure it won't shoot a building instead
            }
        }

    }



}
