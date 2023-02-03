using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

namespace Learning
{
    public class QuizUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private Button nextButton;
        [SerializeField]
        private GameObject infoPanel;
        [SerializeField]
        private GameObject optionPanel;

        private ContentUI current = null;

        private QuizModel quiz;

        private TextMeshProUGUI nextText;

        private InfoUI infoUi;
        private SelectUI selectUi;

        //[SerializeField]
        //private TextAsset file;
        // Start is called before the first frame update
        void Start()
        {
            //todo make reference to the content wrappers
            infoUi = new InfoUI(infoPanel);
            selectUi = new SelectUI(optionPanel);


            nextText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
            nextText.text = "Next";
            title.text = "Bruh";


            var filepath = GameManager.GetInstance().GetNextLearningMinigameFilename();
            var fileContent = Resources.Load<TextAsset>(filepath).ToString();

            var quizState = Quiz.FromXml(fileContent);
            quiz = new QuizModel(quizState);

            // setup handlers
            quiz.SlideChanged += handleNextSlide;
            quiz.QuizSubmitted += handleSubmitted;
            quiz.QuizClosed += handleClosed;

            nextButton.onClick.AddListener(handleNextSelected);

            quiz.start();

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

        private void handleNextSlide(object sender, NextSlideEvent evt)
        {
            var slide = evt.slide;
            this.title.text =  slide.Name;
            var content = slide.Content;

            clearContent();

            if (content is ISelectQuestionState)
            {
                current = selectUi; 

            }
            else if (content is InfoContent)
            {
                current = infoUi;
            }
            current.Start(content);
        }

        private void handleSubmitted(object sender, QuizSubmittedEvent evt)
        {
            clearContent();
            current = infoUi;
            title.text = "SCORE!!!";
            var output = "Your score: " + string.Format("{0:F1}", evt.percentScore) + "%\n" +
                "You " + (evt.pass ? "Passed!!" : " should review the material and try again next time :D\n" +
                "Click next to continue back to the main game");

            InfoContent content = new InfoContent(output);
            current.Start(content);
        }

        private void clearContent() 
        {
            if (current != null)
            {
                current.End();
            }
        }

        private void handleNextSelected()
        {
            this.quiz.next();
        }


    }
}