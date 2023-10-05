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
    // Start is called before the first frame update
    void Start()
    {
        Invoke("susbtsractTime", startingDelay);
    }

    private void susbtsractTime()
    {
        m_Time -= 1;
        m_Text.text = m_Time.ToString();
        if (m_Time <= 0)
        {
            m_Running = false;
        }
        if (m_Running)
        {
            Invoke("susbtsractTime", 1);
        }
        if (!completed && m_Time <= 0)
        {
            SceneManager.LoadScene("test");
        }
    }
}
