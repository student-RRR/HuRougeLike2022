using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �S�Ĥu�t
/// </summary>
public abstract class IEffectFactory
{
    // �إ߯S��
    public abstract IEffect CreateEffect(ENUM_EffectModel _EffectModel);
}

public class EffectFactory : IEffectFactory
{
    public override IEffect CreateEffect(ENUM_EffectModel _EffectModel)
    {
        //IEffect effect = new IEffect();

        //// �]�w�ҫ�
        //GameObject CreatureModel = HuRougeLike2022Factory.GetAssetFactory().LoadEffectModel(_EffectModel);

        //// ���J�ҫ�
        //effect.SetGameObject(UnityEngine.Object.Instantiate(CreatureModel) as GameObject);

        //return effect;
        return null;
    }
}
