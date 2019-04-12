using UnityEngine;

namespace TESTING
{
    public class HumanAI : MonoBehaviour
    {
        float speed = 2;
        public bool isStopped = false;
        public float turnTimer = 1;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!isStopped)
            {
                drive();
            }

            turnTimer += Time.deltaTime;


        }

        public void drive()
        {
            transform.Translate((Vector3.forward * Time.deltaTime) * speed);
        }

        public void TurnLeft()
        {
            if (turnTimer > 0.3f)
            {
                transform.Rotate(transform.rotation.x, -90, transform.rotation.z);
                turnTimer = 0;
            }

        }

        public void TurnRight()
        {
            if (turnTimer > 0.3f)
            {
                transform.Rotate(transform.rotation.x, 90, transform.rotation.z);
                turnTimer = 0;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == ("Car"))
            {
                isStopped = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == ("Car"))
            {
                isStopped = false;
            }
        }


    }
}
