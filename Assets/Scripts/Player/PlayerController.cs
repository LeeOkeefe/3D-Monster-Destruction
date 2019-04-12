using UnityEngine;

namespace Player
{
    internal sealed class PlayerController : MonoBehaviour
    {
        private const float Idle = 0f;
        private const float Walk = 0.5f;
        private const float Run = 1f;

        private Animator m_Animator;
        private PlayerStats m_PlayerStats;

        [SerializeField]
        private BoxCollider rightHandCollider;
        [SerializeField]
        private BoxCollider leftHandCollider;
        [SerializeField]
        private GameObject cameraPivot;
        [SerializeField]
        private float walkSpeed = 15f;
        [SerializeField]
        private float runSpeed = 30f;

        private float MouseSensitivity => GameManager.instance.MouseSensitivity;
        private float Speed => m_Animator.GetFloat("Speed");
        public bool PlayerIsMoving => Speed > 0f;

        private Rigidbody m_Rb;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_PlayerStats = GetComponent<PlayerStats>();
            m_Rb = GetComponent<Rigidbody>();
        }

        // Check input keys every frame to call control methods i.e. sprint, attack and camera control
        //
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift) && m_PlayerStats.CurrentStamina > 0 && PlayerIsMoving ||
                Input.GetKey(KeyCode.RightShift) && m_PlayerStats.CurrentStamina > 0 && PlayerIsMoving)
            {
                Movement(runSpeed);
                m_PlayerStats.DepleteStamina();
            }
            else
            {
                Movement(walkSpeed);
                m_PlayerStats.RegenerateStamina();
            }

            m_Animator.SetBool("CanRun", m_PlayerStats.CurrentStamina > 0);

            if (Input.GetKey(KeyCode.Space))
            {
                Attack();
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

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                direction = (Vector3.left + Vector3.forward).normalized;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                direction = (Vector3.right + Vector3.forward).normalized;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                direction = (Vector3.left + Vector3.back).normalized;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                direction = (Vector3.right + Vector3.back).normalized;
            }

            transform.Translate(direction * Time.deltaTime * movementSpeed);

            // If direction is not equal to zero, use walk animation
            if (direction != Vector3.zero)
            {
                m_Animator.SetFloat("Speed", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? Run : Walk);
            }
            else
            {
                m_Animator.SetFloat("Speed", Idle);
            }
        }

        /// <summary>
        /// Triggers attack animation and the hands collision trigger
        /// </summary>
        private void Attack()
        {
            m_Animator.Play("Attack");

            if (m_Animator.GetBool("IsAttacking"))
            {
                leftHandCollider.isTrigger = true;
                rightHandCollider.isTrigger = true;
                Invoke(nameof(ResetTrigger), 1.5f);
            }
        }

        // Resets the collision triggers
        //
        private void ResetTrigger()
        {
            leftHandCollider.isTrigger = false;
            rightHandCollider.isTrigger = false;
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
