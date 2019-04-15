using UnityEngine;

namespace Traffic_System
{
    public class HumanDirector : MonoBehaviour
    {
        public bool leftTurn;
        public bool rightTurn;
        public bool rightTurnOrForward;
        public bool leftTurnOrForward;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == ("Human"))
            {
                if (leftTurn)
                {
                    //turns car left
                    other.GetComponent<HumanAI>().TurnLeft();
                }
                else if (rightTurn)
                {
                    //turns care right
                    other.GetComponent<HumanAI>().TurnRight();
                }
                else if (leftTurnOrForward)
                {
                    //Choses randomly wether to turn or not
                    int x = Random.Range(0, 2);
                    switch (x)
                    {
                        case 0:
                            other.GetComponent<HumanAI>().TurnLeft();
                            break;
                        case 1:
                            //Go Straight
                            break;
                    }
                }
                else if (rightTurnOrForward)
                {
                    //Choses randomly wether to turn or not
                    int x = Random.Range(0, 2);
                    switch (x)
                    {
                        case 0:
                            other.GetComponent<HumanAI>().TurnRight();
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
