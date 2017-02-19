using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeVideoTexture : MonoBehaviour
{

	public float m_fadeDuration = 2f;

	private bool m_isFadedOut = false;

	public void Fade ()
	{
		if (!m_isFadedOut) {
			StartCoroutine (FadeOut3D (this.transform, 0f, true, m_fadeDuration));
		} else {
			this.transform.gameObject.SetActive (true);
			StartCoroutine (FadeOut3D (this.transform, 1f, false, m_fadeDuration));
		}
		m_isFadedOut = !m_isFadedOut;

	}

	IEnumerator FadeOut3D (Transform _transform, float targetAlpha, bool isVanish, float duration)
	{

		Renderer sr = _transform.GetComponent<Renderer> ();
		float diffAlpha = (targetAlpha - sr.material.color.a);
 
		float counter = 0;
		while (counter < duration) {
			float alphaAmount = sr.material.color.a + (Time.deltaTime * diffAlpha) / duration;
			sr.material.color = new Color (sr.material.color.r, sr.material.color.g, sr.material.color.b, alphaAmount);
 
			counter += Time.deltaTime;
			yield return null;
		}
		sr.material.color = new Color (sr.material.color.r, sr.material.color.g, sr.material.color.b, targetAlpha);
		if (isVanish) {
			sr.transform.gameObject.SetActive (false);
		}
	}
}
