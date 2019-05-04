using System;
using System.Collections;
using System.Collections.Generic;
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

        public bool HoldingObject { get; private set; }

        private Animator anim => GameManager.instance.playerAnim;

        private static Dictionary<string, KeyCode> KeyCodes => GameManager.instance.KeyCodes;

        private bool m_ResetRotation;
        private Tank m_Tank;

        private static readonly int Throw = Animator.StringToHash("Throw");

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
            // Ensure the enemy can always roll back to it's original rotation after being thrown
            // to prevent it getting stuck
            //
            if (m_ResetRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, m_Tank.OriginalRotation, Time.deltaTime * 1);

                if (transform.rotation == m_Tank.OriginalRotation)
                    m_ResetRotation = false;
            }

            if (HoldingObject)
            {
                m_Object.gameObject.transform.position = m_TempParent.transform.position;
            }

            if (Input.GetKeyDown(KeyCodes["Pickup"]) && !HoldingObject)
            {
                PickupObject();
            }
            else if (Input.GetKeyDown(KeyCodes["Pickup"]) && HoldingObject)
            {
                DropObject();
            }
            else if (Input.GetKeyDown(KeyCodes["Throw"]) && HoldingObject)
            {
                ThrowObject();
            }
        }

        /// <summary>
        /// Picks up the object and alters physics whilst picked up
        /// </summary>
        public void PickupObject()
        {
            if (!IsPlayerInRange())
                return;

            if (gameObject.HasComponent<NavMeshAgent>())
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }

            if (gameObject.HasComponent<MoveToTarget>())
            {
                gameObject.GetComponent<MoveToTarget>().CancelInvoke(nameof(MoveToTarget.SetTarget));
            }

            if (gameObject.CompareTag("Human"))
            {
                gameObject.GetComponent<Animator>().enabled = false;
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
            {
                StartCoroutine(nameof(EnableNavMeshAgent));
            }
        }

        /// <summary>
        /// Changes physics, then applies velocity to the rigidBody
        /// </summary>
        private void ThrowObject()
        {
            anim.SetTrigger(Throw);
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
            anim.ResetTrigger("Attack");
            DropObject();
        }

        /// <summary>
        /// Enables NavMeshAgent and sets target again
        /// </summary>
        private IEnumerator EnableNavMeshAgent()
        {
            if (gameObject.HasComponent<Tank>())
            {
                m_Tank = GetComponent<Tank>();
                m_ResetRotation = true;
            }

            yield return new WaitForSeconds(3);

            gameObject.GetComponent<NavMeshAgent>().enabled = true;

            if (gameObject.HasComponent<MoveToTarget>())
            {
                gameObject.GetComponent<NavMeshAgent>().Warp(transform.position);
                gameObject.GetComponent<MoveToTarget>().SetTarget();
            }
        }
    }
}
