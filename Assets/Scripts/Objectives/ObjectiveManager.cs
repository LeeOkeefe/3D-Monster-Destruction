using System;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Objectives
{
    internal sealed class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance { get; private set; }

        public Objective ActiveObjective { get; private set; }

        public DisplayObjective displayObjective;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }

            Instance = this;

            SetObjective();
        }

        /// <summary>
        /// Assigns a new random objective 
        /// </summary>
        private void SetObjective()
        {
            var random = new Random();
            var randomValue = random.Next(5, 16);
            ActiveObjective = new Objective(GetRandomObjectiveType(), randomValue);
        }

        /// <summary>
        /// Returns a random <see cref="ObjectiveType"/>
        /// </summary>
        private static ObjectiveType GetRandomObjectiveType()
        {
            var random = new Random();
            var values = Enum.GetValues(typeof(ObjectiveType));
            var randomValue = random.Next(0, values.Length);
            return (ObjectiveType) randomValue;
        }

        /// <summary>
        /// Increases the objective progress, and checks whether
        /// objective is complete
        /// </summary>
        public void ObjectiveProgressEvent(ObjectiveType objectiveType)
        {
            if (ActiveObjective.ObjectiveType != objectiveType)
                return;

            ActiveObjective.IncreaseProgress();

            if (ActiveObjective.ObjectiveComplete)
            {
                displayObjective.StartCoroutine(nameof(UI.DisplayObjective.TaskComplete));
                ScoreManager.AddScore(1000);
                SetObjective();
            }

            displayObjective.UpdateLabel();
        }
    }
}
