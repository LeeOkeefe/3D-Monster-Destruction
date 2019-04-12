using UnityEngine;

namespace TESTING
{
    public class BridgeController : MonoBehaviour
    {
        [SerializeField]
        private float timer;
        [SerializeField]
        private GameObject barrier1;
        [SerializeField]
        private GameObject barrier2;
        [SerializeField]
        private GameObject bridge1;
        [SerializeField]
        private GameObject bridge2;
        [SerializeField]
        private GameObject roadBlock1;
        [SerializeField]
        private GameObject roadBlock2;

        private Vector3 m_RoadBlock1UpPos;
        private Vector3 m_RoadBlock1DownPos;
        private Vector3 m_RoadBlock2UpPos;
        private Vector3 m_RoadBlock2DownPos;

        private bool m_BridgeUp;

        private void Start()
        {
            m_RoadBlock1UpPos = roadBlock1.transform.position;
            m_RoadBlock1DownPos = new Vector3(m_RoadBlock1UpPos.x, m_RoadBlock1UpPos.y - 1, m_RoadBlock1UpPos.z);

            m_RoadBlock2UpPos = roadBlock2.transform.position;
            m_RoadBlock2DownPos = new Vector3(m_RoadBlock2UpPos.x, m_RoadBlock2UpPos.y - 1, m_RoadBlock2UpPos.z);
        }

        private void Update()
        {
            if (m_BridgeUp)
            {
                roadBlock1.transform.position = m_RoadBlock1DownPos;
                roadBlock2.transform.position = m_RoadBlock2DownPos;

                //lower bar
                barrier1.transform.rotation = Quaternion.Euler(0, 90, 0);
                barrier2.transform.rotation = Quaternion.Euler(0, -90, 0);

                bridge1.transform.rotation = Quaternion.Euler(-90, 90, 0);
                bridge2.transform.rotation = Quaternion.Euler(-90, -90, 0);
                //wait

                //raise bridge
            }
            else
            {
                //lower bridge

                //wait

                //raise bar

                barrier1.transform.rotation = Quaternion.Euler(0, 0, 90);
                barrier2.transform.rotation = Quaternion.Euler(0, 0, 90);
            
                roadBlock1.transform.position = m_RoadBlock1UpPos;
                roadBlock2.transform.position = m_RoadBlock2UpPos;

                bridge1.transform.rotation = Quaternion.Euler(0, 90, 0);
                bridge2.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }
}
