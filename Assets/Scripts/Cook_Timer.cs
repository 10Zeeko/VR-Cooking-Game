using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CookTimer : MonoBehaviour
{
    [SerializeField]
    private int _time = 30;
    [SerializeField]
    private int _startingDelay = 5;
    [SerializeField]
    private bool _running = true;
    [SerializeField]
    private TextMeshProUGUI _text;

    public bool Completed = false;

    void Start()
    {
        Invoke("SubtractTime", _startingDelay);
    }

    void SubtractTime()
    {
        _time -= 1;
        _text.text = _time.ToString();
        if (_time <= 0)
        {
            _running = false;
            if (!Completed)
            {
                Invoke("RestartScene", 3f);
            }
            else
            {
                _text.color = Color.green;
                _running = true;
                _time = 4;
                Invoke("RestartScene", 4f);
            }
        }
        if (_running)
        {
            Invoke("SubtractTime", 1);
        }
    }
    void RestartScene()
    {
        SceneManager.LoadScene("test");
    }
}