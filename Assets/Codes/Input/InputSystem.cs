using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 指令輸入系統
/// </summary>
public class InputSystem : GameSystem
{
    private KeyCode[] usedKeys = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };

    private bool[] keys;

    public InputSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        keys = new bool[usedKeys.Length];
    }

    public KeyCode playInput()
    {
        return KeyCode.A;
    }

    public override void Update()
    {
        for (int i = 0, n = usedKeys.Length; i < n; i++)
            keys[i] = Input.GetKey(usedKeys[i]);
    }
}

