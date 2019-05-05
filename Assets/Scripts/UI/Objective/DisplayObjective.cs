using System.Collections;
using Extensions;
using Objectives;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal sealed class DisplayObjective : MonoBehaviour
    {
        private static Objective ActiveObject => ObjectiveManager.Instance.ActiveObjective;

        [SerializeField]
        private Text objectiveText;
        [SerializeField]
        private CanvasGroup completion;

        private void Start()
        {
            UpdateLabel();
        }

        public void UpdateLabel()
        {
            objectiveText.text = $"Destroy {ActiveObject.ObjectiveType}s \n" +
                                 $" {ActiveObject.CurrentAmount} / {ActiveObject.RequiredAmount}";
        }

        public IEnumerator TaskComplete()
        {
            completion.ToggleGroup(true);

            yield return new WaitForSeconds(1F);

            completion.ToggleGroup(false);
        }
    }
}
