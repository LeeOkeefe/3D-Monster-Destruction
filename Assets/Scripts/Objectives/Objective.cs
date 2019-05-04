using UnityEngine;

namespace Objectives
{
    internal sealed class Objective
    {
        private int m_CurrentAmount;
        private int m_RequiredAmount;
        public ObjectiveType ObjectiveType { get; }
        public bool ObjectiveComplete => m_CurrentAmount >= m_RequiredAmount;

        public Objective(ObjectiveType objectiveType, int requiredAmount)
        {
            m_CurrentAmount = 0;
            ObjectiveType = objectiveType;
            m_RequiredAmount = requiredAmount;
        }

        /// <summary>
        /// Increment the progress of the objective by one
        /// </summary>
        public void IncreaseProgress()
        {
            m_CurrentAmount++;
            Debug.Log(m_CurrentAmount);

            if (ObjectiveComplete)
            {
                Debug.Log("WE DONE");
            }
        }
    }
}
