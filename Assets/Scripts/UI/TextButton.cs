using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    public static Color c = new Color(255, 0, 0, 1);

    private void Start()
    {
        GetComponent<Text>().color = c;
    }
}
