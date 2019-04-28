using System;
using System.Collections;
using AI.Enemies;
using Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Objects.Interactable
{
    internal class ObjectInteraction : InteractableObject
    {
        private GameObject m_TempParent;
        [SerializeField]
        private float throwingForce = 100f;

        private Collider m_Collider;
        private Rigidbody m_Object;

        private static readonly int IsThrowing = Animator.StringToHash("IsThrowing");

        public bool HoldingObject { get; private set; }

        private Animator anim => GameManager.instance.playerAnim;

        private void Start()
        {
            m_Object = GetComponent<Rigidbody>();
            m_Collider = GetComponentInChildren<Collider>();
            m_TempParent = GameManager.instance.playerPickupHand;
        }

        // Check mouse button clicked to determine which method to call
        // i.e. pickup / drop or throw object
        //
        private void Update()
        {
            if (HoldingObject)
            {
                m_Object.gameObject.transform.position = m_TempParent.transform.position;
            }

            if (Input.GetMouseButtonDown(0) && !HoldingObject)
            {
                PickupObject();
            }
            else if (Input.GetMouseButtonDown(0) && HoldingObject)
            {
                DropObject();
            }
            else if (Input.GetMouseButtonDown(1) && HoldingObject)
            {
                ThrowObject();
            }
        }

        /// <summary>
        /// Picks up the object and alters physics whilst picked up
        /// </summary>
        private void PickupObject()
        {
            if (!IsPlayerInRange())
                return;

            if (gameObject.HasComponent<NavMeshAgent>())
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }

            HoldingObject = true;
            m_Object.useGravity = false;
            m_Collider.enabled = false;
        }

        /// <summary>
        /// Resets the physics so the object drops back to the ground
        /// </summary>
        private void DropObject()
        {
            if (!HoldingObject)
            {
                throw new NullReferenceException("Not holding an object");
            }

            m_Object.isKinematic = false;
            HoldingObject = false;
            m_Object.useGravity = true;
            m_Object.transform.parent = null;
            m_Collider.enabled = true;

            if (gameObject.HasComponent<NavMeshAgent>())
                EnableNavMeshAgent();
        }

        /// <summary>
        /// Changes physics, then applies velocity to the rigidBody
        /// </summary>
        private void ThrowObject()
        {
            anim.SetBool(IsThrowing, true);
            StartCoroutine(nameof(Delay));
        }

        // Horrible method to cause a delay so we can use the attack
        // animation so it looks like we are throwing the object.
        // We need to delay the change in settings or the objects
        //
        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.5f);
            m_Object.gameObject.layer = 16;

            m_Object.velocity = Player.transform.forward * throwingForce;
            anim.SetBool(IsThrowing, false);
            DropObject();
        }

        private IEnumerator EnableNavMeshAgent()
        {
            yield return new WaitUntil(() => gameObject.transform.position.y <= 1);

            gameObject.GetComponent<NavMeshAgent>().enabled = true;

            gameObject.GetComponent<MoveToTarget>().SetTarget();
        }
    }
}
