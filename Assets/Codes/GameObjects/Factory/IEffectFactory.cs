using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特效工廠
/// </summary>
public abstract class IEffectFactory
{
    // 建立特效
    public abstract IEffect CreateEffect(ENUM_EffectModel _EffectModel);
}

public class EffectFactory : IEffectFactory
{
    public override IEffect CreateEffect(ENUM_EffectModel _EffectModel)
    {
        //IEffect effect = new IEffect();

        //// 設定模型
        //GameObject CreatureModel = HuRougeLike2022Factory.GetAssetFactory().LoadEffectModel(_EffectModel);

        //// 載入模型
        //effect.SetGameObject(UnityEngine.Object.Instantiate(CreatureModel) as GameObject);

        //return effect;
        return null;
    }
}
