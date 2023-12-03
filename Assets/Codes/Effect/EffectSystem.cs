using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : GameSystem
{

    // �غc�l
    public EffectSystem(HuRougeLikeGame mediator) : base(mediator)
    {
        Initialize();
    }

    // �S��
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
