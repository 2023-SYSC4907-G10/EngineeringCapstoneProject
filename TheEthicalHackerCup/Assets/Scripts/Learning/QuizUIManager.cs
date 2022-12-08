using TMPro;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Learning
{

    public class QuizUIManager : MonoBehaviour
    {
        private QuizModel quiz;
        private QuizState quizState;
        public TextMeshProUGUI title;
        public Button[] buttons = new Button[4];
        public Button nextButton;
        private QuestionModel current = null;
        // Start is called before the first frame update
        void Start()
        {
            var filepath = GameManager.GetInstance().GetNextLearningMinigameFilename();
            var fileContent = Resources.Load<TextAsset>(filepath).ToString();

            quizState = QuizState.fromXml(fileContent);
            quiz = new QuizModel(quizState);


            // setup handlers
            quiz.QuizStarted += handleStart;
            quiz.QuestionChanged += handleNextQuestion;
            quiz.QuizSubmitted += handleSubmitted;
            quiz.QuizClosed += handleClosed;

            //start quiz
            quiz.start();
            showButtons(false);
            for (int i = 0; i < buttons.Length; i++)
            {
                var x = new int();
                x = i;
                buttons[i].onClick.AddListener(() => handleOptionSelected(x));
            }
            nextButton.onClick.AddListener(handleNextSelected);



        }

        // Update is called once per frame
        void Update()
        {

        }

        private void showButtons(bool show)
        {
            int i = 0;
            foreach (var button in buttons)
            {
                button.enabled = show;
                var color = button.colors;
                color.normalColor = Color.white;
                color.selectedColor = Color.white;
                color.highlightedColor = Color.white;
                color.disabledColor = Color.white;
                color.pressedColor = Color.white;
                buttons[i].colors = color;
                if (i++ != 0)
                    button.gameObject.SetActive(show);
            }
        }

        private void handleClosed(object sender, QuizClosedEvent evt)
        {
            Debug.Log(evt.pass ? "Quiz Passed" : "Quiz Failed");
            if (evt.pass)
            {
                GameManager.GetInstance().UpgradeDefenseUpgradeLevel(
                    GameManager.GetInstance().GeNextLearningMinigameSecurityConcept()
                );
            }
            SceneManager.LoadScene("MainScene");
        }

        private void handleStart(object sender, QuizStartedEvent evt)
        {
            title.text = evt.quizName;
            showButtons(false);
            buttons[0].GetComponentInChildren<TMP_Text>().text = "Welcome to the Learning game for " + evt.quizName + ". Press next (>) to start.";
        }
        private void handleNextQuestion(object sender, NextQuestionEvent evt)
        {
            this.current = evt.question;
            this.current.QuestionStateUpdated += handleQuestionStateChanged;
            this.current.invokeQuestionEvent(new StartEvent());
        }
        private void handleSubmitted(object sender, QuizSubmittedEvent evt)
        {
            showButtons(false);
            title.text = "SCORE!!!";
            var output = "Your score: " + string.Format("{0:F1}", evt.percentScore) + "%\n" +
                "You " + (evt.pass ? "Passed!!" : " should review the material and try again next time :D\n" +
                "Click next to continue back to the main game");
            buttons[0].GetComponentInChildren<TMP_Text>().text = output;
        }
        private void handleQuestionStateChanged(object sender, QuestionStateUpdatedEvent evt)
        {
            showButtons(true);
            if (evt.QuestionState.GetType() == typeof(CheckboxState))
            {
                this.handleCheckboxQuestion(sender, evt);
            }
            else if (evt.QuestionState.GetType() == typeof(RadioState))
            {
                this.handleRadioQuestion(sender, evt);
            }
            else
            {
                throw new System.Exception("Invalid State type");
            }
        }


        private void handleCheckboxQuestion(object sender, QuestionStateUpdatedEvent evt)
        {
            var state = (CheckboxState)evt.QuestionState;
            title.text = state.Name;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (state.Options.Count > i)
                {
                    var optionText = (state.Selected.Contains(i) ? "[x] " : "[ ] ") + state.Options[i];
                    buttons[i].GetComponentInChildren<TMP_Text>().text = optionText;

                }
                else
                {
                    buttons[i].enabled = false;
                    buttons[i].gameObject.SetActive(false);
                }
            }

        }

        private void handleRadioQuestion(object sender, QuestionStateUpdatedEvent evt)
        {
            var state = (RadioState)evt.QuestionState;
            title.text = state.Name;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (state.Options.Count > i)
                {
                    var optionText = (state.Selected == i ? "[x] " : "[ ] ") + state.Options[i];
                    buttons[i].GetComponentInChildren<TMP_Text>().text = optionText;

                }
                else
                {
                    buttons[i].enabled = false;
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

        private void handleOptionSelected(int number)
        {
            this.current.invokeQuestionEvent(new SelectEvent(number));
        }

        private void handleNextSelected()
        {
            this.quiz.next();

        }
    }
}