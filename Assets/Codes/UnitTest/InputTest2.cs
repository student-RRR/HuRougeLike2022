using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputTest2 : MonoBehaviour
{
    public bool W;
    public bool S;
    public bool A;
    public bool D;

    void Update()
    {
        W = Input.GetKey(KeyCode.W);
        S = Input.GetKey(KeyCode.S);
        A = Input.GetKey(KeyCode.A);
        D = Input.GetKey(KeyCode.D);

    }

}