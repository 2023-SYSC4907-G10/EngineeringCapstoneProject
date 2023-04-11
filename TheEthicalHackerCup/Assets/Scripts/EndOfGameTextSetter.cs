using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndOfGameTextSetter : MonoBehaviour
{
    public TextMeshProUGUI tmp;

    void Start()
    {
        tmp.text = "Congratulations!\nYou have completed the game!\n";
        tmp.text += (GameManager.GetInstance().GetRespect() >= 50) ?
            "Your team has won through your impressive performance!\nThe judges respect you a lot!\n" :
            "Your team has lost the competition.\nYour performance was respectable, but the judges respect the other team more!\n";

        tmp.text += "\nNevertheless, the company has improved our security,\nthanks to the insights gained through the competition!\n\nEnter your email to export your game summary report";
    }
}
