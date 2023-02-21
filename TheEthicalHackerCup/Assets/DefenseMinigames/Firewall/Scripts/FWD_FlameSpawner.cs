using UnityEngine;

public class FWD_FlameSpawner : MonoBehaviour
{
    public GameObject flamePrefab;
    private Vector3 mousePos;
    private Vector3 objectPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //left mouse click
        {
            mousePos = Input.mousePosition;
            mousePos.z = 2.0f;
            objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(flamePrefab, objectPos, Quaternion.identity);
        }
    }
}
