using UnityEngine;

namespace Traffic_System
{
    internal sealed class CarDirector : MonoBehaviour
    {
        public bool leftTurn;
        public bool rightTurn;
        public bool rightTurnOrForward;
        public bool leftTurnOrForward;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                if (leftTurn)
                {
                    other.GetComponent<CarAI>().TurnLeft();
                }
                else if (rightTurn)
                {
                    other.GetComponent<CarAI>().TurnRight();
                }
                else if (leftTurnOrForward)
                {
                    // Chooses randomly whether to turn or not
                    int x = Random.Range(0, 2);
                    switch (x)
                    {
                        case 0:
                            other.GetComponent<CarAI>().TurnLeft();
                            break;
                        case 1:
                            //Go Straight
                            break;
                    }
                }
                else if (rightTurnOrForward)
                {
                    // Chooses randomly whether to turn or not
                    var x = Random.Range(0, 2);
                    switch (x)
                    {
                        case 0:
                            other.GetComponent<CarAI>().TurnRight();
                            break;
                        case 1:
                            //Go Straight
                            break;
                    }

                }

            }

        }
    }
}
