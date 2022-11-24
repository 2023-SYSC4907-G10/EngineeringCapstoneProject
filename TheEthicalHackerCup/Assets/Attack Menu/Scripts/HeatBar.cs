using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeatBar : MonoBehaviour
{
    private Slider slider;
    private ParticleSystem particles;
    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake(){
        slider = gameObject.GetComponent<Slider>();
        particles = GameObject.Find("Particles").GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress && slider.value < slider.maxValue){
            slider.value += FillSpeed * Time.deltaTime;
            if(!particles.isPlaying){
                particles.Play();
            }
        }else{
            particles.Stop();
        }
        
    }

    public void IncrementProgress(){
        targetProgress = slider.value + 0.10f;
    }
}
