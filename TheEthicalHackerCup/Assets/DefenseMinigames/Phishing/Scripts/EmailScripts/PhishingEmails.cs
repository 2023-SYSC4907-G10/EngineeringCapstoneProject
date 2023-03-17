using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingEmails : MonoBehaviour
{

    [SerializeField] private GameObject[] emails;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject winOrLoseDialog;

    private enum EmailState
    {
        start, showEmail, playerSelection, showAnnotatedEmail, WinOrLoseScreen
    }

    private EmailState state;

    private int randomEmail;
    private bool requiresAnnotationBool = false;

    private bool win = false;

    void Start() {
        state = EmailState.start;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && state == EmailState.showEmail){
            emails[randomEmail].SendMessage("clearEmail");
            player.SendMessage("beginMovingAgain");
            state = EmailState.playerSelection;
        
        } else if (Input.GetMouseButtonDown(0) && state == EmailState.WinOrLoseScreen){
            winOrLoseDialog.SendMessage("hideDialogs");
            if (requiresAnnotationBool) {
                emails[randomEmail].SendMessage("showAnnotatedEmail");
                state = EmailState.showAnnotatedEmail;
            } else {
                if (win) {
                    GameManager.GetInstance().ChangeRespect(10);
                    proceedToAfterActionReport("You have won the game");
                } else {
                    GameManager.GetInstance().ChangeRespect(-10);
                    proceedToAfterActionReport("You have lost the game");
                }

            }
        } else if (Input.GetMouseButtonDown(0) && state == EmailState.showAnnotatedEmail){
            emails[randomEmail].SendMessage("clearEmail");
            if (win) {
                GameManager.GetInstance().ChangeRespect(10);
                proceedToAfterActionReport("You have won the game");
            } else {
                GameManager.GetInstance().ChangeRespect(-10);
                proceedToAfterActionReport("You have lost the game");
            }
        }
    }

    void foundFile() {
        randomEmail = Random.Range(0, emails.Length);
        emails[randomEmail].SendMessage("foundFile");
        state = EmailState.showEmail;
    }

    void playerChoice(bool isPhishing) {
        emails[randomEmail].SendMessage("showWinOrLoseScreen", isPhishing);
        player.SendMessage("goToSelectionAreaAndFeeze");
        state = EmailState.WinOrLoseScreen;
    }

    void requiresAnnotation() {
        requiresAnnotationBool = true;
    }

    private void proceedToAfterActionReport(string afterActionReportMessage)
    {
        GameManager.GetInstance().SwitchToAfterActionReportScene(afterActionReportMessage);
    }

    void wonGame() {
        win = true;
    }

    void lostGame() {
        win = false;
    }
}
