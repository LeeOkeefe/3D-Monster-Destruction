using System.Collections;
using Objectives;
using UI.Settings.Audio;
using UnityEngine;

namespace AI.Human
{
    internal sealed class IdleHuman : MonoBehaviour, IDeathHandler
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

        [SerializeField] private bool scared = true;

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_Animator = GetComponent<Animator>();

            if (scared)
            {
                StartCoroutine(nameof(HandleEffects));
            }
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            HandleDeath();
        }

        // Handle death for human, instantiate particles, add score and destroy gameObject
        //
        public void HandleDeath()
        {
            ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.Human);

            SoundEffectManager.Instance.PlayClipAtPoint(splat, transform.position);
            Instantiate(bloodSplatter, transform.position, Quaternion.Euler(0, 0, 90));
            ScoreManager.AddScore(scoreAwarded);
            Destroy(gameObject);
        }
    }
}
