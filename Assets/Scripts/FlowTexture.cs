using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class FlowTexture : MonoBehaviour
{
    public SubstanceGraph gr;
    public float textureFlowSpeed;
    public string textureParametersName;
    public float minValue;
    public float maxValue;

    void Update()
    {
        gr.SetInputVector2(textureParametersName, new Vector2(Mathf.Lerp(minValue, maxValue, Time.time % textureFlowSpeed / textureFlowSpeed), Mathf.Lerp(minValue, maxValue, Time.time % textureFlowSpeed / textureFlowSpeed)));
        // queue the substance to render
        gr.QueueForRender();
        //render all substancs async
        Substance.Game.Substance.RenderSubstancesAsync();
    }
}


