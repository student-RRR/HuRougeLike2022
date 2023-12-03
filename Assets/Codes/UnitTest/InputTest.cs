using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    private KeyCode[] usedKeys = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };

    public ElementTitle[] elementTitle;

    void Awake()
    {
        elementTitle = new ElementTitle[usedKeys.Length];
    }

    void Update()
    {
        for (int i = 0, n = usedKeys.Length; i < n; i++)
        {
            // ¼ÐÃD
            elementTitle[i].fontName = Enum.GetName(typeof(KeyCode), usedKeys[i]);

            // §PÂ_«öÁä
            elementTitle[i].keys = Input.GetKey((KeyCode)usedKeys[i]);

            //if(elementTitle[i].keys == true)
            //    Debug.Log(Enum.GetName(typeof(KeyCode), usedKeys[i]));
        }
    }

}

[System.Serializable]
public class ElementTitle
{
    [HideInInspector]
    public string fontName = "¬Y­Ó«öÁä";
    public bool keys;
}