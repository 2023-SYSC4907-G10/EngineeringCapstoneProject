using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    [SerializeField] CanvasEnum ce;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ce);
    }

    private void FixedUpdate()
    {
        CanvasEnum canvasEnum = InsiderDefenseSingleton.GetInstance().getCanvasEnum();
        bool isCanvasActive = canvasEnum == ce;
        gameObject.GetComponent<Canvas> ().enabled = isCanvasActive;
    }
}
