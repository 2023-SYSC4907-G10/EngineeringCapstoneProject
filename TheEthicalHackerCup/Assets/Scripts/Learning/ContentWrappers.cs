using Learning;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


abstract class ContentUI
{
    private GameObject panel;

    public ContentUI(GameObject panel) 
    {
        this.panel = panel;

    }
    public void Start(IContent content) 
    {
        panel.SetActive(true);
        processModel(content);
    }

    protected abstract void processModel(IContent content);

    public void End() 
    {
        panel.SetActive(false);
    }

}

class InfoUI : ContentUI
{
    private TextMeshProUGUI text;
    private InfoContent content;
    public InfoUI(GameObject panel) : base(panel)
    {
        text = panel.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void processModel(IContent content)
    {
        this.content = (InfoContent)content;
        this.text.text = this.content.Info;
    }
}

class SelectUI : ContentUI
{
    private SelectableQuestionUI questionUI;

    public SelectUI(GameObject panel) : base(panel)
    {
        questionUI = panel.GetComponent<SelectableQuestionUI>();
    }

    protected override void processModel(IContent content)
    {
        var state = (ISelectQuestionState)content;
        var model = (SelectionModel)QuestionModel.GenerateModel(state);
        questionUI.Init(model);
    }
}