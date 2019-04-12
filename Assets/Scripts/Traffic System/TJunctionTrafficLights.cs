using UnityEngine;

namespace TESTING
{
    public class TJunctionTrafficLights : MonoBehaviour
    {
        public float timer;
        public GameObject roadBlock1;
        public GameObject roadBlock2;
        public GameObject roadBlock3;

        Vector3 roadBlock1UpPos;
        Vector3 roadBlock1DownPos;

        Vector3 roadBlock2UpPos;
        Vector3 roadBlock2DownPos;

        Vector3 roadBlock3UpPos;
        Vector3 roadBlock3DownPos;
    

        public GameObject lightSet1Green;
        public GameObject lightSet1Red;

        public GameObject lightSet2Green;
        public GameObject lightSet2Red;

        public GameObject lightSet3Green;
        public GameObject lightSet3Red;
    


        // Use this for initialization
        void Start()
        {
            timer = (Random.Range(0f, 14f));

            roadBlock1UpPos = roadBlock1.transform.position;
            roadBlock1DownPos = new Vector3(roadBlock1UpPos.x, roadBlock1UpPos.y - 1, roadBlock1UpPos.z);

            roadBlock2UpPos = roadBlock2.transform.position;
            roadBlock2DownPos = new Vector3(roadBlock2UpPos.x, roadBlock2UpPos.y - 1, roadBlock2UpPos.z);

            roadBlock3UpPos = roadBlock3.transform.position;
            roadBlock3DownPos = new Vector3(roadBlock3UpPos.x, roadBlock3UpPos.y - 1, roadBlock3UpPos.z);
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer > 0 && timer < 4)
            {
                roadBlock1.transform.position = roadBlock1UpPos;
                roadBlock2.transform.position = roadBlock2DownPos;
                roadBlock3.transform.position = roadBlock3DownPos;

                lightSet1Green.SetActive(true);
                lightSet1Red.SetActive(false);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);

            }
            else if (timer > 5 && timer < 9)
            {
                roadBlock1.transform.position = roadBlock1DownPos;
                roadBlock2.transform.position = roadBlock2UpPos;
                roadBlock3.transform.position = roadBlock3DownPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(true);
                lightSet2Red.SetActive(false);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);
            
            }
            else if (timer > 10 && timer < 14)
            {
                roadBlock1.transform.position = roadBlock1DownPos;
                roadBlock2.transform.position = roadBlock2DownPos;
                roadBlock3.transform.position = roadBlock3UpPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(true);
                lightSet3Red.SetActive(false);   
            }
            else if (timer > 15)
            {
                timer = 0;
            }
            else
            {
                roadBlock1.transform.position = roadBlock1DownPos;
                roadBlock2.transform.position = roadBlock2DownPos;
                roadBlock3.transform.position = roadBlock3DownPos;

                lightSet1Green.SetActive(false);
                lightSet1Red.SetActive(true);

                lightSet2Green.SetActive(false);
                lightSet2Red.SetActive(true);

                lightSet3Green.SetActive(false);
                lightSet3Red.SetActive(true);
            }

        }
    }
}
