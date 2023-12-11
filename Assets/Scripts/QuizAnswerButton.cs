using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAnswerButton : MonoBehaviour
{
    private int answerIndex = 0;

    public void SetAnswerIndex(int index) => answerIndex = index;
    public int GetAnswerIndex() => answerIndex;
}
