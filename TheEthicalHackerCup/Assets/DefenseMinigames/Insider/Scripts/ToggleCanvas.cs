using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    [SerializeField] CanvasEnum ce;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        CanvasEnum canvasEnum = InsiderDefenseSingleton.GetInstance().getCanvasEnum();
        bool isCanvas = canvasEnum == ce;
        gameObject.SetActive(isCanvas);
    }
}
