using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionScriptableObject> questions;
    [SerializeField] private GameObject answerButtonGroup;
    [SerializeField] private GameObject answerButtonPrefab;

    [SerializeField] private Color defaultColorButton;
    [SerializeField] private Color correctColorButton;
    [SerializeField] private Color incorrectColorButton;

    private QuestionScriptableObject actualQuestion;

    private void Start()
    {
        SetupQuestion(questions[0]);
    }

    public async Task OnAnswerSelected(int index)
    {
        SetButtonsCanClickState(false);

        GameObject selectedButton = answerButtonGroup.transform.GetChild(index).gameObject;
        Image imageComponent = selectedButton.GetComponent<Image>();

        if (index == actualQuestion.GetCorrectAnswerIndex())
            imageComponent.color = correctColorButton;
        else
            imageComponent.color = incorrectColorButton;

        await Task.Delay(millisecondsDelay: 1000);

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
