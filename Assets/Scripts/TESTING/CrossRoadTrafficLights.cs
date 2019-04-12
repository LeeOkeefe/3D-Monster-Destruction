using UnityEngine;

namespace TESTING
{
    public class CrossRoadTrafficLights : MonoBehaviour
    {
        public float timer;
        public GameObject roadBlock1;
        public GameObject roadBlock2;
        public GameObject roadBlock3;
        public GameObject roadBlock4;

        private Vector3 m_RoadBlock1UpPos;
        private Vector3 m_RoadBlock1DownPos;
        private Vector3 m_RoadBlock2UpPos;
        private Vector3 m_RoadBlock2DownPos;
        private Vector3 m_RoadBlock3UpPos;
        private Vector3 m_RoadBlock3DownPos;
        private Vector3 m_RoadBlock4UpPos;
        private Vector3 m_RoadBlock4DownPos;

        public GameObject lightSet1Green;
        public GameObject lightSet1Red;

        public GameObject lightSet2Green;
        public GameObject lightSet2Red;

        public GameObject lightSet3Green;
        public GameObject lightSet3Red;

        public GameObject lightSet4Green;
        public GameObject lightSet4Red;


        // Use this for initialization
        void Start()
        {
            timer = (Random.Range(0f, 14f));

            m_RoadBlock1UpPos = roadBlock1.transform.position;
            m_RoadBlock1DownPos = new Vector3(m_RoadBlock1UpPos.x, m_RoadBlock1UpPos.y - 1, m_RoadBlock1UpPos.z);

            m_RoadBlock2UpPos = roadBlock2.transform.position;
            m_RoadBlock2DownPos = new Vector3(m_RoadBlock2UpPos.x, m_RoadBlock2UpPos.y - 1, m_RoadBlock2UpPos.z);

            m_RoadBlock3UpPos = roadBlock3.transform.position;
            m_RoadBlock3DownPos = new Vector3(m_RoadBlock3UpPos.x, m_RoadBlock3UpPos.y - 1, m_RoadBlock3UpPos.z);

            m_RoadBlock4UpPos = roadBlock4.transform.position;
            m_RoadBlock4DownPos = new Vector3(m_RoadBlock4UpPos.x, m_RoadBlock4UpPos.y - 1, m_RoadBlock4UpPos.z);
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if(timer > 0 && timer < 4)
            {
                roadBlock1.transform.position = m_RoadBlock1UpPos;
                roadBlock2.transform.position = m_RoadBlock2DownPos;
                roadBlock3.transform.position = m_RoadBlock3DownPos;
                roadBlock4.transform.position = m_RoadBlock4DownPos;

                lightSet1Green.SetActive(true);
                lightSet1Red.SetActive(false);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);

                lightSet4Green.SetActive(false);
                lightSet4Red.SetActive(true);

            }
            else if (timer > 5 && timer < 9)
            {
                roadBlock1.transform.position = m_RoadBlock1DownPos;
                roadBlock2.transform.position = m_RoadBlock2UpPos;
                roadBlock3.transform.position = m_RoadBlock3DownPos;
                roadBlock4.transform.position = m_RoadBlock4DownPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(true);
                lightSet2Red.SetActive(false);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);

                lightSet4Green.SetActive(false);
                lightSet4Red.SetActive(true);
            }
            else if (timer > 10 && timer < 14)
            {
                roadBlock1.transform.position = m_RoadBlock1DownPos;
                roadBlock2.transform.position = m_RoadBlock2DownPos;
                roadBlock3.transform.position = m_RoadBlock3UpPos;
                roadBlock4.transform.position = m_RoadBlock4DownPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(true);
                lightSet3Red.SetActive(false);

                lightSet4Green.SetActive(false);
                lightSet4Red.SetActive(true);
            }
            else if (timer > 15 && timer < 19)
            {
                roadBlock1.transform.position = m_RoadBlock1DownPos;
                roadBlock2.transform.position = m_RoadBlock2DownPos;
                roadBlock3.transform.position = m_RoadBlock3DownPos;
                roadBlock4.transform.position = m_RoadBlock4UpPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);

                lightSet4Green.SetActive(true);
                lightSet4Red.SetActive(false);
            }
            else if (timer > 20)
            {
                timer = 0;
            }
            else
            {
                roadBlock1.transform.position = m_RoadBlock1DownPos;
                roadBlock2.transform.position = m_RoadBlock2DownPos;
                roadBlock3.transform.position = m_RoadBlock3DownPos;
                roadBlock4.transform.position = m_RoadBlock4DownPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);

                lightSet4Green.SetActive(false);
                lightSet4Red.SetActive(true);
            }

        }
    }
}
