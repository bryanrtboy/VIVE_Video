using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FadeVideoTexture : MonoBehaviour
{

	public float m_fadeDuration = 5f;

	private bool m_isFadedOut = true;
    private bool m_fadeIsHappeningNow = false;
    private VideoController m_controller;
    private Transform m_transform;
    VideoPlayer vp;

    private void Start()
    {
        m_transform = this.transform;
        m_controller = FindObjectOfType<VideoController>() as VideoController;
        vp = this.GetComponent<VideoPlayer>() as VideoPlayer;
    }

    public void Fade ()
	{
       // this.StopAllCoroutines();
        if (m_controller != null && !vp.isPlaying)
          m_controller.StopAllPlayingVideos();

        if (m_fadeIsHappeningNow)
            return;

        m_fadeIsHappeningNow = true;

		if (!m_isFadedOut) {
			StartCoroutine (FadeOut3D ( 0f, true, m_fadeDuration));
            Debug.Log("Going to fade out now");
		} else {
			//this.transform.gameObject.SetActive (true);
			StartCoroutine (FadeOut3D (1f, false, m_fadeDuration));
            if(!vp.isPlaying)
                vp.Play();
		}
		m_isFadedOut = !m_isFadedOut;

	}

    public void FadeOutNow()
    {
        this.StopAllCoroutines();
        StartCoroutine(FadeOut3D(0f, true, m_fadeDuration));
        m_isFadedOut = true;
    }

	IEnumerator FadeOut3D (float targetAlpha, bool isVanish, float duration)
	{

		Renderer sr = m_transform.GetComponent<Renderer> ();
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
                vp.Pause();
                vp.time = 0f;
            // sr.transform.gameObject.SetActive (false);
        } 

        m_fadeIsHappeningNow = false;
	}
}
