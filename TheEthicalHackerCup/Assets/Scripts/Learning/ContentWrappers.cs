using Learning;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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

    public virtual void End() 
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

class ImageUI : ContentUI
{
    private Image image;
    private ImageContent content;
    public ImageUI(GameObject panel) : base(panel)
    {
        image = panel.GetComponent<Image>();
    }

    protected override void processModel(IContent content)
    {
        this.content = (ImageContent)content;
        var sprite = Resources.Load<Sprite>(this.content.ImageLocation);
        image.sprite = sprite;
        image.overrideSprite = sprite;
    }
}


class VideoUI : ContentUI
{
    private VideoPlayer videoPlayer;
    public VideoUI(GameObject panel) : base(panel)
    {
        videoPlayer = panel.transform.GetComponent<VideoPlayer>();
    }

    protected override void processModel(IContent content)
    {
        var vid = (VideoContent)content;
        this.videoPlayer.url = vid.VideoLocation;
        videoPlayer.Play();
    }

    public override void End()
    {
        videoPlayer.Stop();
        base.End();
    }


}