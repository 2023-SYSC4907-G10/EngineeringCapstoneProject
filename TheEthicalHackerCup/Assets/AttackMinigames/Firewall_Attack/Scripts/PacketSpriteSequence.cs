using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PacketSpriteSequence : MonoBehaviour
{
    public List<GameObject> layers;

    public TextMeshPro packetTextMeshPro;

    private int _currentLayerIndex;


    // Start is called before the first frame update
    void Start()
    {
        // Hide All of them initially
        foreach (GameObject layer in layers)
        {
            layer.SetActive(false);
        }

        // Set the initial one to active
        _currentLayerIndex = 0;
        layers[0].SetActive(true);
        packetTextMeshPro.SetText(layers[_currentLayerIndex].name);

    }

    public void SwitchToNextPacketLayer()
    {
        // Switch to the next layer, unless we're on the last layer
        if (_currentLayerIndex + 1 < layers.Count)
        {
            layers[_currentLayerIndex].SetActive(false);
            _currentLayerIndex++;
            layers[_currentLayerIndex].SetActive(true);
            packetTextMeshPro.SetText(layers[_currentLayerIndex].name);
        }
    }
}
