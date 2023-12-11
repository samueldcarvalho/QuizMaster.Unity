using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private QuestionScriptableObject question;
    [SerializeField] private GameObject answerButtonGroup;
    [SerializeField] private GameObject answerButtonPrefab;

    private void Start()
    {
        questionText.text = question.GetQuestion();

        foreach (string answer in question.GetAnswers())
        {
            var instantiatedObject = Instantiate(answerButtonPrefab, answerButtonGroup.transform);
            var textMeshPro = instantiatedObject.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = answer;
        } 
    }
}
