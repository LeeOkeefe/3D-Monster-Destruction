using AI.Waypoints;
using System.Collections;
using Extensions;
using Objectives;
using UI.Settings.Audio;
using UnityEngine;

namespace AI.Human
{
    internal sealed class Human : WaypointAI, IDeathHandler
    {

        [SerializeField]
        private GameObject bloodSplatter;
        [SerializeField]
        private float scoreAwarded = 5;
        [SerializeField]
        private AudioClip scream;
        [SerializeField]
        private AudioClip splat;

        private AudioSource m_AudioSource;

        private Animator m_Animator;
        private static readonly int Terrified = Animator.StringToHash("Terrified");

        private void Start()
        {
            Initialize();
            m_Animator = GetComponent<Animator>();
            m_AudioSource = GetComponent<AudioSource>();
            StartCoroutine(nameof(HandleEffects));
        }

        private void Update()
        {
            Movement();
        }

        /// <summary>
        /// Handles animation and audio when player is in or out of range
        /// </summary>
        private IEnumerator HandleEffects()
        {
            yield return new WaitUntil(() => GameManager.Instance.IsPlayerInRange(transform, 10));

            m_Animator.SetBool(Terrified, true);
            SoundEffectManager.Instance.PlayClipAtPoint(scream, transform.position);

            yield return new WaitUntil(() => !GameManager.Instance.IsPlayerInRange(transform, 10));

            m_Animator.SetBool(Terrified, false);
            StartCoroutine(nameof(HandleEffects));
        }

        // Handle death if we collide with the player
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            HandleDeath();
        }

        // Set the next waypoint and keep track of our previous one
        //
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Waypoint") || !(Vector3.Distance(Transform.position, m_Waypoints[WaypointIndex].position) < 1))
                return;

            if (other.GetInstanceID() == m_LastWaypointId)
                return;

            WaypointIndex++;

            if (WaypointIndex >= m_Waypoints.Count)
            {
                WaypointIndex = 0;
            }

            m_LastWaypointId = other.GetInstanceID();
        }

        // Handle death for human
        //
        public void HandleDeath()
        {
            AudioSource.PlayClipAtPoint(splat, Camera.main.transform.position);
            Instantiate(bloodSplatter, transform.position, Quaternion.Euler(0, 0, 90));
            ScoreManager.AddScore(scoreAwarded);
            Destroy(gameObject);

            ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.Human);
        }
    }
}
