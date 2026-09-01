using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI countdownTimer;
    public float intialTime = 60f;
    public float remainingTime;
    public static TimeManager Instance;

    private void Awake()
    {
        remainingTime = intialTime;
        Instance = this;
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            countdownTimer.color = Color.red;
            //StartCoroutine(MoveToGameOverScreen());
        }


        //int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        //countdownTimer.text = string.Format("{0:00}: {1:00}", minutes, seconds);
        countdownTimer.text = seconds.ToString();
    }

    IEnumerator MoveToGameOverScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(1);
    }

    public void AddTwoSeconds() => remainingTime += 2f;
}
