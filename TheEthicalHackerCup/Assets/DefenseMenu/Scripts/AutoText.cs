using UnityEngine;
using System.Collections;
using TMPro;
 
public class AutoText : MonoBehaviour{
    public static bool autoTextTyping;
    public static void TypeText(TextMeshProUGUI textElement, string text, float time){
        float characterDelay = time / text.Length;
        autoTextTyping = true;
        textElement.StartCoroutine(SetText(textElement, text, characterDelay));
    }
 
    static IEnumerator SetText(TextMeshProUGUI textElement, string text, float characterDelay){
        for(int i=0; i<text.Length; i++){
            textElement.text += text[i];
            yield return new WaitForSeconds(characterDelay);
        }
        yield break;
    }
}