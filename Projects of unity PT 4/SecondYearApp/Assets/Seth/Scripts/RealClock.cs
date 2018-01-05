using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealClock : MonoBehaviour
{
    public int Minutes = 0;
    public int Seconds = 0;

    private Text m_text;
    private float m_leftTime;
    public float otherTime;

    public Canvas questionCanvas;
    public string unsuccessfulText;
    public Text finishedText;


    private void Start()
    {


 
    }


    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_leftTime = GetInitialTime();
    }

    private void Update()
    {
            //Updating other Clock
            

        if (m_leftTime > 0f)
        {


            //  Update countdown clock
            m_leftTime -= Time.deltaTime;
            Minutes = GetLeftMinutes();
            Seconds = GetLeftSeconds();

            //  Show current clock

            m_text.text = "Time : " + Minutes + ":" + Seconds.ToString("00");
            gameObject.GetComponentInParent<Canvas>().enabled = true;
            finishedText.GetComponent<Text>().enabled = false;
        }
        else
        {

            //  The countdown clock has finished
            m_text.text = "Time : 0:00";

        }

        if (m_leftTime < 0f)
        {

            finishedText.GetComponent<Text>().enabled = true;
            unsuccessfulText = "You didn't finish this in time.";
            finishedText.GetComponent<Text>().text = unsuccessfulText;
            otherTime += Time.deltaTime;
            if (otherTime >= 11.0f)
            {
                gameObject.GetComponentInParent<Canvas>().enabled = false;

            }



        }

        
    }

    private float GetInitialTime()
    {
        return Minutes * 60f + Seconds;
    }

    private int GetLeftMinutes()
    {
        return Mathf.FloorToInt(m_leftTime / 60f);
    }

    private int GetLeftSeconds()
    {
        return Mathf.FloorToInt(m_leftTime % 60f);
    }
}