using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class FlowTexture : MonoBehaviour
{
    public SubstanceGraph gr;

    void Update()
    {
        gr.SetInputVector2("Lava_Flowing", new Vector2(Mathf.Lerp(-0.5f, 0.5f, Time.time % 10 / 10), Mathf.Lerp(-0.5f, 0.5f, Time.time % 10 / 10)));
        // queue the substance to render
        gr.QueueForRender();

        //render all substancs async
        Substance.Game.Substance.RenderSubstancesAsync();
    }

    private void OnBecameVisible()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnBecameInvisible()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}


