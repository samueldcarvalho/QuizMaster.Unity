using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimer : MonoBehaviour
{
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float delayToShowCorrectAnswer = 2f;
    [SerializeField] private bool paused = true;

    private float timerValue = 0;
    private Image imageComponent;

    public static event Action OnTimerCompleted;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
    }

    private void Update()
    {
        if(timerValue > 0 && paused == false)
            UpdateTimer();
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue <= 0)
        {
            timerValue = 0;
            OnTimerCompleted();
        }

        imageComponent.fillAmount = timerValue / timeToCompleteQuestion;
    }

    public void StartTimer()
    {
        timerValue = timeToCompleteQuestion;
        paused = false;
    }

    public void SetPausedTimer(bool isPaused) => 
        paused = isPaused;

    public float GetDelayToShowCorrectAnswer() => delayToShowCorrectAnswer;
}
