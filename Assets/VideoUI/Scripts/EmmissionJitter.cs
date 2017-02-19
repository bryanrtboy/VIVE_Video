using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmissionJitter : MonoBehaviour
{

	public float m_emissionScale = 1.0F;
	public float m_speed = 1.0F;
    public Color baseColor = Color.white;

    Material m_mat;
	

	// Use this for initialization
	void Start ()
	{
		Renderer m_renderer = this.GetComponent<Renderer> () as Renderer;

		if (m_renderer == null)
			Destroy (this);

		m_mat = m_renderer.material;

	}
	
	// Update is called once per frame
	void Update ()
	{

		float emission = m_emissionScale * Mathf.PerlinNoise (Time.time * m_speed, 0.0F);

		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
 
		m_mat.SetColor ("_EmissionColor", finalColor);

		
	}



}
