using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScoreManager : MonoBehaviour
{
    [SerializeField] private int scorePerCorrectAnswer = 12;
    private int actualScore = 0;

    private void Start()
    {
        QuizManager.OnSelectedOption += QuizManager_OnSelectedOption;
    }

    private void QuizManager_OnSelectedOption(object sender, bool e)
    {
        if (e == true)
            actualScore += scorePerCorrectAnswer;
    }

    public int GetActualScore() => actualScore;
}
