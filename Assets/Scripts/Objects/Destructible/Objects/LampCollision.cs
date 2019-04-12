using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCollision : MonoBehaviour
{
    
    public GameObject lamp;

    bool destroyed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !destroyed)
        {
            if (lamp != null)
            {
                lamp.transform.Rotate(Vector3.forward, 90, Space.Self);
                lamp.transform.Rotate(Vector3.right, 90, Space.Self);
            }
            
            destroyed = true;
        }
    }
}
