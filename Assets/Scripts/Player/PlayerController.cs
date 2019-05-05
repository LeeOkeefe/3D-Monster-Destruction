using System.Collections.Generic;
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

        [SerializeField]
        private AudioClip woosh;
        [SerializeField]
        private AudioClip hit;

        private AudioSource m_AudioSource;

        private static readonly int AnimationSpeed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private static float MouseSensitivity => GameManager.Instance.MouseSensitivity;
        private float Speed => m_Animator.GetFloat(AnimationSpeed);
        public bool PlayerIsMoving => Speed > 0f;

        private static Dictionary<string, KeyCode> KeyCodes => GameManager.Instance.KeyCodes;

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_PlayerStats = GetComponent<PlayerStats>();
            m_AudioSource = GetComponent<AudioSource>();
        }

        // Check input keys every frame to call control methods i.e. sprint, attack and camera control
        //
        private void Update()
        {
            if (m_PlayerStats.IsDead)
                return;

            if (Input.GetKey(KeyCodes["Sprint"]) && m_PlayerStats.CanRun && PlayerIsMoving)
            {
                Movement(runSpeed);
                m_PlayerStats.DepleteStamina();
            }
            else
            {
                Movement(walkSpeed);
                m_PlayerStats.RegenerateStamina();
            }

            if (Input.GetKey(KeyCodes["Punch"]))
            {
                if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    m_Animator.SetTrigger(Attack);
                    m_AudioSource.PlayOneShot(woosh);
                }
            }

            if (Input.GetKey(KeyCodes["Jump"]))
            {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    return;

                m_Animator.SetTrigger(Jump);
            }

            CameraControl();
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
            if (Input.GetKey(KeyCodes["Right"]))
            {
                direction = Vector3.right;
            }
            else if (Input.GetKey(KeyCodes["Left"]))
            {
                direction = Vector3.left;
            }
            else if (Input.GetKey(KeyCodes["Forward"]))
            {
                direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCodes["Back"]))
            {
                direction = Vector3.back;
            }

            // Handles diagonal movement with combined input keys
            //
            if (Input.GetKey(KeyCodes["Left"]) && Input.GetKey(KeyCodes["Forward"]))
            {
                direction = (Vector3.left + Vector3.forward);
            }
            else if (Input.GetKey(KeyCodes["Right"]) && Input.GetKey(KeyCodes["Forward"]))
            {
                direction = (Vector3.right + Vector3.forward);
            }
            else if (Input.GetKey(KeyCodes["Left"]) && Input.GetKey(KeyCodes["Back"]))
            {
                direction = (Vector3.left + Vector3.back);
            }
            else if (Input.GetKey(KeyCodes["Right"]) && Input.GetKey(KeyCodes["Back"]))
            {
                direction = (Vector3.right + Vector3.back);
            }

            transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);

            // Handles the Idle/Walk/Run animations based on conditions
            //
            if (direction != Vector3.zero && m_PlayerStats.CanRun && Input.GetKey(KeyCodes["Sprint"]))
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
