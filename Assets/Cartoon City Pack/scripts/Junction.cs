using System.Collections.Generic;
using UnityEngine;

internal sealed class Junction : MonoBehaviour
{
	public bool free;
	public bool waiting;

    [SerializeField]
	private Renderer trafficLight;

	private const int Red = 1;
	private const int Yellow = 2;
	private const int Green = 3;

    private IList<Material> m_Materials; 
	
	private void Start()
    {
        m_Materials = new List<Material>(trafficLight.materials);

        foreach (var material in m_Materials)
        {
            material.color = Color.grey;
        }

        m_Materials[0].color = Color.black;
	}

	private void Update ()
	{
		if (free)
		{
            m_Materials[Green].color = Color.green;
            m_Materials[Yellow].color = Color.grey;
		}
		else if (waiting)
		{
            m_Materials[Yellow].color = Color.yellow;
            m_Materials[Red].color = Color.grey;
            m_Materials[Green].color = Color.grey;
		}
		else
		{
            m_Materials[Red].color = Color.red;
            m_Materials[Yellow].color = Color.grey;
        }
	}
}
