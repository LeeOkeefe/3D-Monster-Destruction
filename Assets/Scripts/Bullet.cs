using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particles;
    [SerializeField]
    GameObject bulletMesh;


    [SerializeField]
    bool explode = false;

    float timer;



    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!explode)
        {
            transform.Translate(Vector3.forward);
        }
        else
        {
            particles.Play();
            bulletMesh.SetActive(false);
            Destroy(gameObject, 0.3f);
        }

        if (timer > 5)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        explode = true;
    }
}
