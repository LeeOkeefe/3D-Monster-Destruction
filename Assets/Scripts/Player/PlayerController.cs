using UnityEngine;

namespace Player
{
    internal sealed class PlayerController : MonoBehaviour
    {
        private const float Idle = 0F, Walk = 0.5F, Run = 1F;

        private Animator m_Animator;
        private PlayerStats m_PlayerStats;

        [SerializeField]
        private GameObject cameraPivot;
        [SerializeField]
        private float walkSpeed = 12.5F, runSpeed = 20F;

        private static readonly int AnimationSpeed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private static float MouseSensitivity => GameManager.instance.MouseSensitivity;
        private float Speed => m_Animator.GetFloat(AnimationSpeed);
        public bool PlayerIsMoving => Speed > 0f;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_PlayerStats = GetComponent<PlayerStats>();
        }

        // Check input keys every frame to call control methods i.e. sprint, attack and camera control
        //
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift) && m_PlayerStats.CanRun && PlayerIsMoving ||
                Input.GetKey(KeyCode.RightShift) && m_PlayerStats.CanRun && PlayerIsMoving)
            {
                Movement(runSpeed);
                m_PlayerStats.DepleteStamina();
            }
            else
            {
                Movement(walkSpeed);
                m_PlayerStats.RegenerateStamina();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    m_Animator.SetTrigger(Attack);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    return;

                m_Animator.SetTrigger(Jump);
            }

            if (Input.GetMouseButton(2) || Input.GetKey(KeyCode.LeftAlt))
            {
                CameraControl();
            }
        }

        /// <summary>
        /// Reads player input and changes the direction based on input.
        /// Sets the movement in the set direction using the specified speed
        /// </summary>
        private void Movement(float movementSpeed)
        {
            var direction = Vector3.zero;

            // Handles the direction based on the input keys
            //
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction = Vector3.back;
            }

            // Handles diagonal movement with combined input keys
            //
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                direction = (Vector3.left + Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                direction = (Vector3.right + Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                direction = (Vector3.left + Vector3.back);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                direction = (Vector3.right + Vector3.back);
            }

            transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);

            // Handles the Idle/Walk/Run animations based on conditions
            //
            if (direction != Vector3.zero && m_PlayerStats.CanRun && Input.GetKey(KeyCode.LeftShift) ||
                direction != Vector3.zero && m_PlayerStats.CanRun && Input.GetKey(KeyCode.RightShift))
            {
                m_Animator.SetFloat(AnimationSpeed, Run);
            }
            else if (direction != Vector3.zero && !m_PlayerStats.CanRun ||
                     direction != Vector3.zero && m_PlayerStats.CanRun)
            {
                m_Animator.SetFloat(AnimationSpeed, Walk);
            }
            else
            {
                if (m_Animator.IsInTransition(0))
                    return;

                m_Animator.SetFloat(AnimationSpeed, Idle);
            }
        }

        /// <summary>
        /// Ensures the cursor is on the screen. Then we get the Y & X axis of the mouse,
        /// rotate the camera pivot and the transform rotation accordingly.
        /// Restrict how far we can pivot to prevent seeing underneath the map
        /// </summary>
        private void CameraControl()
        {
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);

            if (!screenRect.Contains(Input.mousePosition))
                return;

            var v = Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
            var h = Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;

            cameraPivot.transform.Rotate(-v, 0, 0);
            transform.Rotate(0, h, 0);

            if (cameraPivot.transform.localEulerAngles.x <= 320 && cameraPivot.transform.localEulerAngles.x >= 90)
            {
                cameraPivot.transform.localEulerAngles = new Vector3(320, 0, 0);
            }
            if (cameraPivot.transform.localEulerAngles.x >= 35 && cameraPivot.transform.localEulerAngles.x <= 90)
            {
                cameraPivot.transform.localEulerAngles = new Vector3(35, 0, 0);
            }
        }
    }
}
