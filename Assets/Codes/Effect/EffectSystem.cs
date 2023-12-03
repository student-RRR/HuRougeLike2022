using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : GameSystem
{

    // 建構子
    public EffectSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
    }

    // 特效
    public static void Effect(IEffect effect, Position2D pos_s, Position2D pos_e)
    {
        effect.GetGameObject().transform.position = new Vector2(pos_e.x, pos_e.y);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Release()
    {
        base.Release();
    }

    public override void Update()
    {
        base.Update();
    }
}
