using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Play_script : MonoBehaviour
{
    int i = 0;
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            m_IsOneClick = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            i++;
            if(!m_IsOneClick)
            {
                m_Timer = Time.time;
                m_IsOneClick = true;
            }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                GetComponent<VideoPlayer>().Pause();
            }
            if (i % 2 == 1)
            {
                GetComponent<VideoPlayer>().Play();
            }
        }
        
    }
}
