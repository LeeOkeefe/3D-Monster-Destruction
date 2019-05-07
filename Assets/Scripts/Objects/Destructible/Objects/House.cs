using AI;
using Objectives;
using Objects.Destructible.Definition;
using UnityEngine;
using UI.Settings.Audio;

namespace Objects.Destructible.Objects
{
    internal sealed class House : DestructibleObject
    {
        [SerializeField]
        private GameObject destroyedHouse;
        [SerializeField]
        private GameObject particlePosition;
        [SerializeField]
        private GameObject particleEffect;
        [SerializeField]
        AudioClip collapse;

        // Instantiate particle effect and enable/disable building + destruction
        // - Don't like this way, but the models transforms are all messed up
        // and it'll be too time consuming to fix it all
        //
        public override void Destruct()
        {
            ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.Building);
            Instantiate(particleEffect, particlePosition.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            destroyedHouse.gameObject.SetActive(true);
            Destroy(this);
        }

        // Check the player is colliding, then call destruct
        // and award the points for destruction
        //
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destruct();
                SoundEffectManager.Instance.PlayClipAtPoint(collapse, transform.position);

                ScoreManager.AddScore(scoreAwarded);
            }

            if (!other.gameObject.GetComponent(typeof(IDeathHandler)))
                return;

            var death = other.gameObject.GetComponent(typeof(IDeathHandler)) as IDeathHandler;
            death?.HandleDeath();
            Destruct();
        }
    }
}