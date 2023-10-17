using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cook_Timer : MonoBehaviour
{
    [SerializeField]
    private int m_Time = 30;
    [SerializeField]
    private int startingDelay = 5;
    [SerializeField]
    private bool m_Running = true;
    [SerializeField]
    private TextMeshProUGUI m_Text;

    public bool completed = false;

    void Start()
    {
        Invoke("SubtractTime", startingDelay);
    }

    void SubtractTime()
    {
        m_Time -= 1;
        m_Text.text = m_Time.ToString();
        if (m_Time <= 0)
        {
            m_Running = false;
            if (!completed)
            {
                Invoke("RestartScene", 3f);
            }
            else
            {
                m_Text.color = Color.green;
                m_Running = true;
                m_Time = 4;
                Invoke("RestartScene", 4f);
            }

        }
        if (m_Running)
        {
            Invoke("SubtractTime", 1);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene("test");
    }
}
