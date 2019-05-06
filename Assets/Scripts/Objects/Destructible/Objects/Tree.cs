using AI.Enemies;
using Objectives;
using Objects.Destructible.Definition;
using UI.Settings.Audio;
using UnityEngine;

namespace Objects.Destructible.Objects
{
    internal sealed class Tree : DestructibleObject
    {
        [SerializeField]
        private GameObject particleEffect;
        [SerializeField]
        private GameObject treeStump;
        [SerializeField]
        private GameObject fireEffect;
        [SerializeField]
        private GameObject burntStump;
        [SerializeField]
        private AudioClip audioClip;

        // Deduct health by existing health, add score and check to DestroyObject,
        // instantiate particle effects if we are destroyed
        //
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") || 
                other.gameObject.CompareTag("Car") || 
                other.gameObject.CompareTag("Tank") && other.transform.rotation != other.GetComponent<Tank>().OriginalRotation)
            {
                SoundEffectManager.Instance.PlayClipAtPoint(audioClip, transform.position);
                ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.Tree);
                currentHealth -= currentHealth;
                AddScore();
                Destroy(gameObject, 0.1F);
            }

            //if (other.gameObject.CompareTag("Flamethrower"))
            //{
               // BurnTree();
            //}

            if (IsObjectDestroyed)
            {
                Destruct();
            }
        }

        // Set health to be 0 and instantiate particle effects
        // for burning trees
        //
        public void BurnTree()
        {
            currentHealth -= currentHealth;
            var position = transform.position;

            Instantiate(fireEffect, position, Quaternion.identity);
            AddScore();
            DestroyObject();
            Instantiate(burntStump, position, Quaternion.identity);
        }

        // Instantiate particle effect and gameObject, then check health
        //
        public override void Destruct()
        {
            SoundEffectManager.Instance.PlayClipAtPoint(audioClip, transform.position);

            var position = transform.position;
            Instantiate(particleEffect, position, Quaternion.identity);
            Instantiate(treeStump, position, Quaternion.identity);
            Destroy(gameObject, 0.1F);
        }
    }
}
