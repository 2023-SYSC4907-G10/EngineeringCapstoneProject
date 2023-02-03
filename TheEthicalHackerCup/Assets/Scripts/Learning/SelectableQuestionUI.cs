using System.Collections.Generic;
using UnityEngine;
using Learning;
using UnityEngine.UI;
using TMPro;

public class SelectableQuestionUI : MonoBehaviour
{
    [SerializeField]
    GameObject buttonPrefab;
    private IList<GameObject> buttons = new List<GameObject>();
    private SelectionModel model;
    bool start = true;
    //private SelectionModel model;
    // Start is called before the first frame update
    public void Start()
    {
        //RadioBox box = new RadioBox(1, new List<string> { "Opt 1", "Opt 2", "Opt 3"}, 1);
        //Init(QuestionModel.GenerateModel(box) as SelectionModel);
    }

    public void Init(SelectionModel model)
    {
        
        this.model = model;
        model.QuestionStateUpdated += questionUpdated;
        model.invokeQuestionEvent(new StartEvent());


    }

    private void questionUpdated(object sender, QuestionStateUpdatedEvent evt) 
    {
        var state = (ISelectQuestionState)evt.QuestionState;
        if (start)
        {
            deleteButtons();
            makeButtons(state);
            updateButtons(state);
            start = false;
        }
        else 
        {
            updateButtons(state);
        }
    }

    private void updateButtons(ISelectQuestionState state)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            var option = state.GetOptions()[i];
            button.GetComponentInChildren<TextMeshProUGUI>().text = (state.isSelected(i) ? "[x]" : "[ ]") + option;

        }

    }

    private void makeButtons(ISelectQuestionState state)
    {
        for (int i = 0; i < state.GetOptions().Count; i++)
        {
            var button = GameObject.Instantiate(buttonPrefab);
            button.transform.SetParent(this.transform, false);
            var x = new int();
            x = i;
            button.GetComponent<Button>().onClick.AddListener(() => handleOptionSelected(x));
            buttons.Add(button);
        }
    }

    private void deleteButtons() 
    {
        for (var i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }
        buttons.Clear();
    
    }

    private void handleOptionSelected(int number)
    {
        this.model.invokeQuestionEvent(new SelectEvent(number));
    }

}
