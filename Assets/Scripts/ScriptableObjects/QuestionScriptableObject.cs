using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/New Question", fileName = "New question")]
public class QuestionScriptableObject : ScriptableObject
{
    [TextArea(1, 6)]
    [SerializeField] private string question = "Enter the question here";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int correctAnswerIndex = 0;

    public int GetCorrectAnswerIndex() => correctAnswerIndex;
    public string GetQuestion() => question;
    public string[] GetAnswers() => answers;
    public bool ValidateAnswer(int answerIndex) => correctAnswerIndex == answerIndex;
}
