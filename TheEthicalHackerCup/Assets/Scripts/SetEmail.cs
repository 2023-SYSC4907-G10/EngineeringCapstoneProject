using UnityEngine;
using UnityEngine.UI;

public class SetEmail : MonoBehaviour {
    private string input;
    public void ReadStringInput(string s) {
        input = s;
    }

    public void SaveEmail(string s) {
        GameManager.GetInstance().SetPlayerEmail(input);
    }
}