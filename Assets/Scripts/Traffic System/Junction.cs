using System.Collections.Generic;
using UnityEngine;

namespace Traffic_System
{
    internal sealed class Junction : MonoBehaviour
    {
        public bool Free { get; private set; }
        public bool Waiting { get; private set; }

        [SerializeField]
        private Renderer trafficLight;

        private Dictionary<string, Color> m_Colours;

        private IList<Material> m_Materials; 
	
        private void Start()
        {
            m_Materials = new List<Material>(trafficLight.materials);

            m_Colours = new Dictionary<string, Color>
            {
                { "Red", Color.red },
                { "Yellow", Color.yellow },
                { "Green", Color.green }
            };

            ResetLights();
        }

        private void Update ()
        {
            if (Free)
            {
                SwitchLight(TrafficColour.Green);
            }
            else if (Waiting)
            {
                SwitchLight(TrafficColour.Yellow);
            }
            else
            {
                SwitchLight(TrafficColour.Red);
            }
        }

        /// <summary>
        /// Switches traffic light colour to specified one,
        /// and resets the others
        /// </summary>
        private void SwitchLight(TrafficColour trafficColour)
        {
            var value = (int) trafficColour;
            var colourString = trafficColour.ToString();

            var color = m_Colours[colourString];

            ResetLights();

            foreach (var material in m_Materials)
            {
                if (material == m_Materials[value])
                {
                    m_Materials[value].color = color;
                }
            }
        }

        /// <summary>
        /// Sets the traffic lights to grey (disabled)
        /// </summary>
        public void ResetLights()
        {
            foreach (var material in m_Materials)
            {
                material.color = Color.grey;
            }

            m_Materials[0].color = Color.black;
        }

        /// <summary>
        /// Set the state of the traffic lights
        /// </summary>
        public void SetTrafficState(bool free, bool waiting)
        {
            Free = free;
            Waiting = waiting;
        }
    }
}
