using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 疭
public enum ENUM_Effect
{
    attack
}

public class IEffect
{
    IAssetFactory assetFactory = HuRougeLike2022Factory.GetAssetFactory();
    GameObject effectObject;

    // 篶
    public IEffect(ENUM_Effect _effect)
    {
        ENUM_EffectModel enum_effectModel = new ENUM_EffectModel();
        switch (_effect)
        {
            case ENUM_Effect.attack:
                {
                    enum_effectModel = ENUM_EffectModel.ModelAttack;
                    break;
                }
            default:
                break;
        }

        // 砞﹚家
        GameObject CreatureModel = assetFactory.LoadEffectModel(enum_effectModel);

        // 更家
        SetGameObject(UnityEngine.Object.Instantiate(CreatureModel) as GameObject);

    }


    // 砞﹚Unity家
    public void SetGameObject(GameObject theGameObject)
    {
        effectObject = theGameObject;

        GameObject.Destroy(effectObject, 0.1f);
    }

    public GameObject GetGameObject()
    {
        return effectObject;
    }

}
