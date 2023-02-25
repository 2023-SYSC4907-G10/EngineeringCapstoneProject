using UnityEngine;

public class FWD_FlameSpawner : MonoBehaviour
{
    public GameObject flamePrefab;
    private Vector3 mousePos;
    private Vector3 objectPos;
    private bool _isPregameState;

    // Start is called before the first frame update
    void Start()
    {
        _isPregameState = FWD_Manager.GetInstance().GetIsPregameState();
        FWD_Manager.OnPregameStateChange += SetIsPregameState;
    }


    // Update is called once per frame
    void Update()
    {
        if (!_isPregameState && Input.GetButtonDown("Fire1")) //left mouse click
        {
            mousePos = Input.mousePosition;
            mousePos.z = 2.0f;
            objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(flamePrefab, objectPos, Quaternion.identity);
        }
    }

    void SetIsPregameState(bool isPregameState)
    {
        _isPregameState = isPregameState;
    }
    
    void OnDestroy()
    {
        FWD_Manager.OnPregameStateChange -= SetIsPregameState;
    }
}
