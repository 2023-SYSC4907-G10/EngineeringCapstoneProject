using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    [SerializeField] CanvasEnum ce;

    private void FixedUpdate()
    {
        CanvasEnum canvasEnum = InsiderDefenseSingleton.GetInstance().getCanvasEnum();
        bool isCanvasActive = canvasEnum == ce;
        gameObject.GetComponent<Canvas> ().enabled = isCanvasActive;
    }
}
