using UnityEngine;

namespace Objectives
{
    internal sealed class Objective
    {
        public int CurrentAmount { get; private set; }
        public int RequiredAmount { get; }
        public ObjectiveType ObjectiveType { get; }
        public bool ObjectiveComplete => CurrentAmount >= RequiredAmount;

        public Objective(ObjectiveType objectiveType, int requiredAmount)
        {
            CurrentAmount = 0;
            ObjectiveType = objectiveType;
            RequiredAmount = requiredAmount;
        }

        /// <summary>
        /// Increment the progress of the objective by one
        /// </summary>
        public void IncreaseProgress()
        {
            CurrentAmount++;
            Debug.Log(CurrentAmount);

            if (ObjectiveComplete)
            {
                Debug.Log("WE DONE");
            }
        }
    }
}
