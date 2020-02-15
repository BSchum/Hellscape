using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class FlowTexture : MonoBehaviour
{
    public Renderer rend;
    SubstanceGraph gr;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        if (rend.isVisible)
        {
            Material mat = null;
            mat = rend.sharedMaterial;
            gr = SubstanceGraph.Find(mat);
            gr.SetInputVector2("Lava_Flowing", new Vector2(Mathf.Lerp(-0.5f, 0.5f, Time.time % 10 / 10), Mathf.Lerp(-0.5f, 0.5f, Time.time % 10 / 10)));
            // queue the substance to render
            gr.QueueForRender();

            //render all substancs async
            Substance.Game.Substance.RenderSubstancesAsync();
        }
    }
}


