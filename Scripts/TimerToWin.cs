using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerToWin : MonoBehaviour
{
    public Text timerText;
    [SerializeField]
    private int timeLeft;
    [SerializeField]
    private int timeFromStart;
    private int timeAtStart;
    private bool justOnce = true;


    public GameObject winPanel;
    public SignScript signScript;


    void Start()
    {
        signScript = GameObject.FindGameObjectWithTag("Sign").GetComponent<SignScript>();
        justOnce = true;
        timeAtStart = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeFromStart = (int)Time.time-timeAtStart;
        timeLeft = 180 - timeFromStart;
        if (timeLeft>0)
            timerText.text = "Seconds left: " + timeLeft.ToString();
        if (timeLeft <= 0 && justOnce == true)
        {
            signScript.won = true;
            winPanel.SetActive(true);
            justOnce = false;            
        }
    }

    void TimerReset()
    {
        //Time.time = 0;
    }
}
