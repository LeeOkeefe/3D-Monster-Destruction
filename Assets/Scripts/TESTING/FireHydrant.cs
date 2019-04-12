using UnityEngine;

namespace TESTING
{
    public class FireHydrant : MonoBehaviour
    {
        public GameObject model;
        public GameObject brokenModel;
        public ParticleSystem particles;
        public float time = 7;
        public bool destroyed = false;
    

        // Use this for initialization
        void Start()
        {
            particles.startSpeed = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (destroyed)
            {
                Destroyed();
            }

        }

        void Destroyed()
        {
            model.SetActive(false);
            brokenModel.SetActive(true);
            particles.Play();
            time -= Time.deltaTime;

            if (time > 6)
            {
                particles.startSpeed += (Time.deltaTime * 10);
            }

            if (time < 3)
            {
                particles.startSpeed -= Time.deltaTime;
                particles.emissionRate -= (Time.deltaTime * 20);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                destroyed = true;
            }
        }
    }
}
