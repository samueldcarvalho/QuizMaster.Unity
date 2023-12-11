using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private List<QuestionScriptableObject> questions;
    [SerializeField] private GameObject answerButtonGroup;
    [SerializeField] private GameObject answerButtonPrefab;
    [SerializeField] private QuizTimer timer;

    [SerializeField] private Color defaultColorButton;
    [SerializeField] private Color selectedColorButton;
    [SerializeField] private Color correctColorButton;
    [SerializeField] private Color incorrectColorButton;

    public static event EventHandler<bool> OnSelectedOption;

    private QuestionScriptableObject actualQuestion;
    private QuizScoreManager quizScoreManager;

    private void Awake()
    {
        quizScoreManager = GetComponent<QuizScoreManager>();
    }

    private void Start()
    {
        SetupQuestion(questions[0]);
    }

    private void Update() =>
        valueText.text = quizScoreManager.GetActualScore().ToString();

    public async Task OnAnswerSelected(int index)
    {
        SetButtonsCanClickState(false);
        timer.SetPausedTimer(true);

        GameObject selectedButton = answerButtonGroup.transform.GetChild(index).gameObject;
        Image imageComponent = selectedButton.GetComponent<Image>();

        imageComponent.color = selectedColorButton;

        await Task.Delay((int)Mathf.Round(timer.GetDelayToShowCorrectAnswer()) * 1000);

        if (index == actualQuestion.GetCorrectAnswerIndex())
        {
            imageComponent.color = correctColorButton;
            OnSelectedOption(this, true);
        }
        else
        {
            GameObject correctButton = answerButtonGroup.transform.GetChild(actualQuestion.GetCorrectAnswerIndex()).gameObject; 

            correctButton.GetComponent<Image>().color = correctColorButton;
            imageComponent.color = incorrectColorButton;

            OnSelectedOption(this, false);
        }

        await Task.Delay(2000);

        Clear();
        SetupQuestion(questions[questions.IndexOf(actualQuestion) + 1]);
    }

    private void SetupQuestion(QuestionScriptableObject nextQuestion)
    {
        actualQuestion = nextQuestion;

        questionText.text = actualQuestion.GetQuestion();

        List<string> answers = actualQuestion.GetAnswers().ToList();

        foreach (string answer in answers)
        {
            var instantiatedObject = Instantiate(answerButtonPrefab, answerButtonGroup.transform);

            instantiatedObject.GetComponent<QuizAnswerButton>()
                .SetAnswerIndex(answers.IndexOf(answer));

            var imageComponent = instantiatedObject.GetComponent<Image>();
            var buttonComponent = instantiatedObject.GetComponent<Button>();
            var textMeshProComponent = instantiatedObject.GetComponentInChildren<TextMeshProUGUI>();

            buttonComponent.onClick.AddListener(async delegate { await OnAnswerSelected(answers.IndexOf(answer)); });
            imageComponent.color = defaultColorButton;
            textMeshProComponent.text = answer;
        }

        SetButtonsCanClickState(true);
        timer.StartTimer();
    }

    private void Clear()
    {
        foreach (Transform child in answerButtonGroup.transform)
            Destroy(child.gameObject);
    }

    private void SetButtonsCanClickState(bool canClick)
    {
        for (int i = 0; i < actualQuestion.GetAnswers().Length; i++)
        {
            var button = answerButtonGroup.transform.GetChild(i);
            var buttonComponent = button.GetComponent<Button>();

            buttonComponent.interactable = canClick;
        }
    }
}
