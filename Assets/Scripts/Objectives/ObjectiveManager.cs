using System;
using UnityEngine;
using Random = System.Random;

namespace Objectives
{
    internal sealed class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance { get; private set; }

        private Objective m_ActiveObjective;

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
            var randomValue = random.Next(5, 21);
            m_ActiveObjective = new Objective(GetRandomObjectiveType(), randomValue);
            Debug.Log($"Objective Type: {m_ActiveObjective.ObjectiveType}, Objective Amount: {randomValue}");
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
            if (m_ActiveObjective.ObjectiveType != objectiveType)
                return;

            m_ActiveObjective.IncreaseProgress();

            if (m_ActiveObjective.ObjectiveComplete)
            {
                ScoreManager.AddScore(1000);
                SetObjective();
            }
        }
    }
}
