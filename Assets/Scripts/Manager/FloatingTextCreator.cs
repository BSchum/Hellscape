using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextCreator : MonoBehaviour
{
    public Canvas canvas;
    public GameObject floatingTextPrefab;
    public static FloatingTextCreator instance;

    public void CreateFloatingText(string text, Transform location)
    {
        GameObject floatingText = Instantiate(floatingTextPrefab);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        floatingText.transform.SetParent(canvas.transform, false);
        floatingText.transform.position = screenPos;
        floatingText.GetComponentInChildren<Text>().text = text;
    }

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
