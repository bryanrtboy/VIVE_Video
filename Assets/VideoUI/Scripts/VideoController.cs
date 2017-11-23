using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer[] m_players;
    public FadeVideoTexture[] m_screens;

    void Awake()
    {
        m_players = FindObjectsOfType<VideoPlayer>();

        m_screens = new FadeVideoTexture[m_players.Length];

        for(int i = 0; i < m_players.Length;i++)
        {
            FadeVideoTexture f = m_players[i].gameObject.GetComponent<FadeVideoTexture>() as FadeVideoTexture;
            m_screens[i] = f;
        }

        SteamVR_Render.instance.pauseGameWhenDashboardIsVisible = false;
    }

    public void StopAllPlayingVideos()
    {
        for(int i =0; i < m_players.Length; i++)
        {
            if(m_players[i].isPlaying)
            {
                m_screens[i].FadeOutNow();
            } 
 
        }
    }
}
